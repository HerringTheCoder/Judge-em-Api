using System.Linq;
using System.Threading.Tasks;
using Core.Requests;
using Core.Services.Interfaces;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryRatingRepository _categoryRatingRepository;

        public RatingService(IRatingRepository ratingRepository, ICategoryRepository categoryRepository, ICategoryRatingRepository categoryRatingRepository)
        {
            _ratingRepository = ratingRepository;
            _categoryRepository = categoryRepository;
            _categoryRatingRepository = categoryRatingRepository;
        }

        public async Task AddRating(RatingCreateRequest request, string userId)
        {
            if (int.TryParse(userId, out int id))
            {
                var category = _categoryRepository.Get(c => c.Id == request.CategoryId).First();
                var rating = new Rating()
                {
                    ItemId = request.ItemId,
                    UserId = id
                };
                _ratingRepository.Add(rating);
                var categoryRating = new CategoryRating
                {
                    Score = request.Score,
                    Category = category,
                    Rating = rating
                };
                _categoryRatingRepository.Add(categoryRating);
                await _ratingRepository.SaveChangesAsync();
                await _categoryRatingRepository.SaveChangesAsync();
            }
        }
    
    }
}
