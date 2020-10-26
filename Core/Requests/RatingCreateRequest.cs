using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class RatingCreateRequest
    {
        [Required]
        public List<CategoryRatingCreateRequest> CategoryRatings { get; set; }

        public int ItemId { get; set; }
        public string PlayerProfileId { get; set; }
    }
}
