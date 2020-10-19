using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class CategoryUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Weight { get; set; }
    }
}
