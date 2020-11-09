using System.Text.Json.Serialization;

namespace Core.Helpers
{
    public class PlayerEntry
    {
        [JsonIgnore]
        public string Group { get; set; }
        public string Nickname { get; set; }
    }
}
