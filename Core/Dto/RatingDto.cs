using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using Storage.Tables;

namespace Core.Dto
{
    public class RatingDto
    {
        public int Id { get; set; }
        public float TotalScore { get; set; }
        public UserDto User { get; set; }

        public RatingDto(Rating rating)
        {
            Id = rating.Id;
            TotalScore = rating.TotalScore;
            User = new UserDto(rating.User);
        }
    }
}
