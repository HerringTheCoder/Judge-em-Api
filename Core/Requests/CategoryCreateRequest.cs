using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class CategoryCreateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Weight { get; set; }
    }
}
