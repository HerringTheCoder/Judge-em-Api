using System.Collections.Generic;
using Storage.Tables;

namespace Core.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public ICollection<RatingDto> Ratings { get; set; }

        public ItemDto(Item item)
        {
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            ImageLink = item.ImageLink;
            Ratings = new List<RatingDto>();
            foreach (var rating in item.Ratings)
            {
                Ratings.Add(new RatingDto(rating));
            }
        }
    }
}
