using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Storage.Tables
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int GameId { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }
        [JsonIgnore]
        public ICollection<CategoryRating> CategoryRatings { get; set; }
    }
}
