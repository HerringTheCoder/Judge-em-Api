using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using Storage.Tables;

namespace Core.Dto
{
    public class RatingDto
    {
        public int Id { get; set; }
        public float TotalScore { get; set; }
        public PlayerProfileDto PlayerProfile { get; set; }

        public RatingDto(Rating rating)
        {
            Id = rating.Id;
            TotalScore = rating.TotalScore;
            PlayerProfile = new PlayerProfileDto(rating.PlayerProfile);
        }
    }
}
