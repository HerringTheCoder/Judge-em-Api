using System.Collections.Generic;

namespace Storage.Tables
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public ICollection<CategoryRating> CategoryRatings { get; set; }
    }
}
