using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class GameCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
