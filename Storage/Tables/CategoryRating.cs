using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Tables
{
    public class CategoryRating
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        public int Score { get; set; }
    }
}
