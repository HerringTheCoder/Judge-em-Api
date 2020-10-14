using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class ItemCreateRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
    }
}
