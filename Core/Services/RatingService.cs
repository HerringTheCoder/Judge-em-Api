using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Requests;
using Core.Services.Interfaces;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly ICategoryRatingRepository _categoryRatingRepository;
        private readonly ICategoryRepository _categoryRepository;

        public RatingService(IRatingRepository ratingRepository, ICategoryRatingRepository categoryRatingRepository, ICategoryRepository categoryRepository)
        {
            _ratingRepository = ratingRepository;
            _categoryRatingRepository = categoryRatingRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddRating(RatingCreateRequest request)
        {
            var categoryRatings = request.CategoryRatings;
                var categories = _categoryRepository
                    .GetAll()
                    .Where(c => categoryRatings
                        .Select(cr => cr.CategoryId)
                        .Contains(c.Id))
                    .ToList();
                var scoreWeights = categoryRatings.Join(categories, cr => cr.CategoryId, c => c.Id, (cr, c) => new ScoreWeight { Score = cr.Score, Weight = c.Weight }).ToList();
                var rating = new Rating
                {
                    ItemId = request.ItemId,
                    PlayerProfileId = request.PlayerProfileId,
                    TotalScore = GetTotalScore(scoreWeights)
                };
                _ratingRepository.Add(rating);
                await _ratingRepository.SaveChangesAsync();
                AddCategoryRatings(categoryRatings, rating.Id);
        }

        private float GetTotalScore(List<ScoreWeight> scoreWeights)
        {
            float weightedValueSum = scoreWeights.Sum(sw => sw.Score * sw.Score);
            float weightSum = scoreWeights.Sum(sw => sw.Weight);

            return weightSum != 0 ? weightedValueSum / weightSum : 0;
        }

        private async void AddCategoryRatings(IEnumerable<CategoryRatingCreateRequest> categoryRatings, int ratingId)
        {
            var oldCategoryRatings = _categoryRatingRepository.GetAll()
                .Where(cr => cr.RatingId == ratingId)
                .Where(cr => categoryRatings.Any(c => c.CategoryId == cr.CategoryId))
                .ToList();

            foreach (var categoryRating in categoryRatings)
            {
                int categoryId = categoryRating.CategoryId;
                if (oldCategoryRatings.Any(ocr => ocr.CategoryId == categoryId))
                {
                    var oldCategoryRating = oldCategoryRatings.First(ocr => ocr.CategoryId == categoryId);
                    oldCategoryRating.Score = categoryRating.Score;
                    _categoryRatingRepository.Update(oldCategoryRating);
                }
                else
                {
                    _categoryRatingRepository.Add(new CategoryRating
                    {
                        Score = categoryRating.Score,
                        CategoryId = categoryRating.CategoryId,
                        RatingId = ratingId
                    });
                }
            }
            await _ratingRepository.SaveChangesAsync();
        }

    }
}
