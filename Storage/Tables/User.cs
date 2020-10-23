using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Storage.Tables
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string ProviderId { get; set; }
        public string ProviderName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Game> Games { get; set; }
        [JsonIgnore]
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
