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

        public async Task AddRating(RatingCreateRequest request, int userId)
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
                    UserId = userId,
                    TotalScore = GetTotalScore(scoreWeights)
                };
                _ratingRepository.Add(rating);
                await _ratingRepository.SaveChangesAsync();
                AddCategoryRatings(categoryRatings, rating.Id);
        }

        private float GetTotalScore(List<ScoreWeight> scoreWeights)
        {
            float totalScore = scoreWeights.Aggregate<ScoreWeight, float>(0, (current, scoreWeight) => current + scoreWeight.Score * scoreWeight.Weight);
            return totalScore != 0 ? totalScore / scoreWeights.Count() : 0;
        }

        private async void AddCategoryRatings(IEnumerable<CategoryRatingCreateRequest> categoryRatings, int ratingId)
        {
            foreach (var categoryRating in categoryRatings)
            {
                _categoryRatingRepository.Add(new CategoryRating
                {
                    Score = categoryRating.Score,
                    CategoryId = categoryRating.CategoryId,
                    RatingId = ratingId
                });
            }
            await _ratingRepository.SaveChangesAsync();
        }

    }
}
