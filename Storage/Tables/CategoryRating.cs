using System.Text.Json.Serialization;

namespace Storage.Tables
{
    public class CategoryRating
    {
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public int RatingId { get; set; }
        [JsonIgnore]
        public Rating Rating { get; set; }
        public int Score { get; set; }
    }
}
