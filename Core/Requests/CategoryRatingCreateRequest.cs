using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class CategoryRatingCreateRequest
    {
        [Required]
        public float Score { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
