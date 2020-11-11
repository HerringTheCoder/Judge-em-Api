using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Requests;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly ICategoryRatingRepository _categoryRatingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<RatingService> _logger;

        public RatingService(IRatingRepository ratingRepository, ICategoryRatingRepository categoryRatingRepository, ICategoryRepository categoryRepository, ILogger<RatingService> logger)
        {
            _logger = logger;
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
            float totalScore = GetTotalScore(scoreWeights);

            var rating = await _ratingRepository
                .Get(r => r.PlayerProfileId == request.PlayerProfileId && r.ItemId == request.ItemId)
                .FirstOrDefaultAsync();

            if (rating == null)
            {
                rating = new Rating
                {
                    ItemId = request.ItemId,
                    PlayerProfileId = request.PlayerProfileId,
                    TotalScore = totalScore
                };
                _ratingRepository.Add(rating);
            }
            else
            {
                rating.TotalScore = totalScore;
                _ratingRepository.Update(rating);
            }

            await _ratingRepository.SaveChangesAsync();
            await AddCategoryRatings(categoryRatings, rating.Id);
        }

        private float GetTotalScore(List<ScoreWeight> scoreWeights)
        {
            float weightedValueSum = scoreWeights.Sum(sw => sw.Score * sw.Weight);
            float weightSum = scoreWeights.Sum(sw => sw.Weight);

            return weightSum != 0 ? weightedValueSum / weightSum : 0;
        }

        private async Task AddCategoryRatings(IEnumerable<CategoryRatingCreateRequest> categoryRatings, int ratingId)
        {
            try
            {
                var oldCategoryRatings = _categoryRatingRepository.GetAll()
                    .Where(ocr => ocr.RatingId == ratingId)
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
            }
            catch (Exception ex)
            {
                _logger.LogError("CategoryRating write error");
                _logger.LogError(ex.Message);
            }

            await _ratingRepository.SaveChangesAsync();
        }

    }
}
