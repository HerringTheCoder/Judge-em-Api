using System.Collections.Generic;

namespace Storage.Tables
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int? ProviderId { get; set; }
        public string ProviderName { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
