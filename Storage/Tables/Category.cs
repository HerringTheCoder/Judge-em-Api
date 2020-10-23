using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Storage.Tables
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }
        [JsonIgnore]
        public ICollection<CategoryRating> CategoryRatings { get; set; }
    }
}
