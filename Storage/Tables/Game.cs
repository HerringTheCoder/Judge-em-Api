using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Storage.Tables
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        [ForeignKey(nameof(User))]
        public int MasterId { get; set; }
        [JsonIgnore]
        public User Master { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<Item> Items { get; set; }
        [JsonIgnore]
        public ICollection<Category> Categories { get; set; }
        [JsonIgnore]
        public ICollection<PlayerProfile> PlayerProfiles { get; set; }
    }
}
