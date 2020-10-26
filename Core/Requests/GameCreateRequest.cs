using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class GameCreateRequest
    {
        [Required]
        [StringLength(30, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Name { get; set; }
    }
}
