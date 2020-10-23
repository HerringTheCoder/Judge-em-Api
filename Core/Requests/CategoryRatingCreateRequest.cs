using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class CategoryRatingCreateRequest
    {
        [Required]
        public int Score { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
