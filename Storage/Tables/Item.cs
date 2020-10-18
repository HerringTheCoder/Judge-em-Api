using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Tables
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
