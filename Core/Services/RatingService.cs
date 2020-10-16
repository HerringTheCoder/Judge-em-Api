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

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task AddRating(RatingCreateRequest request, string userId)
        {

            if (int.TryParse(userId, out int id))
            {
                var rating = new Rating
                {
                    CategoryId = request.CategoryId,
                    ItemId = request.ItemId,
                    Score = request.Score,
                    UserId = id
                };
                _ratingRepository.Add(rating);
                await _ratingRepository.SaveChangesAsync();
            }
        }
    
    }
}
