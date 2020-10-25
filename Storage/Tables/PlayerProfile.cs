using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Storage.Tables
{
    public class PlayerProfile
    {
        [Key]
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int GameId { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }
        [JsonIgnore]
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
