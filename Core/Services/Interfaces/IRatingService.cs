using System.Threading.Tasks;
using Core.Requests;

namespace Core.Services.Interfaces
{
    public interface IRatingService
    {
        Task AddRating(RatingCreateRequest request, int userId);
    }
}
