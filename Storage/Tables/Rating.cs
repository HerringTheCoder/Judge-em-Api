using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Tables
{
    public class Rating
    {
        public int Id { get; set; }
        public float TotalScore { get; set; }
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public Item Item { get; set; }
        [ForeignKey(nameof(PlayerProfile))]
        public string PlayerProfileId { get; set; }
        public PlayerProfile PlayerProfile { get; set; }
        public ICollection<CategoryRating> CategoryRatings { get; set; }
    }
}
