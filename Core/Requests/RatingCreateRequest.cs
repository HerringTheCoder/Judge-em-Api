using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class RatingCreateRequest
    {
        [Required]
        public int Score { get; set; }
        [Required]
        public int ItemId { get; set; }
        public int CategoryId { get; set; }
    }
}
