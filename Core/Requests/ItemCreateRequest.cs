using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class ItemCreateRequest
    {
        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(255, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public int GameId { get; set; }
    }
}
