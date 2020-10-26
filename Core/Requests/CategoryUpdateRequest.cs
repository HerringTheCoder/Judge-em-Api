using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class CategoryUpdateRequest : CategoryCreateRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
