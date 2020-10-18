using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Tables
{
    public class Summary
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        public string Result { get; set; }
    }
}
