using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Storage.Tables
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        [JsonIgnore]
        public ICollection<CategoryRating> CategoryRatings { get; set; }
    }
}
