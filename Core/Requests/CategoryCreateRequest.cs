using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class CategoryCreateRequest
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [Range(1, 5)]
        public int Weight { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}
