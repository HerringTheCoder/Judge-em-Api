using System;
using System.Collections.Generic;
using Storage.Tables;

namespace Core.Dto
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserDto Master { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public List<ItemDto> Items { get; set; }

        public GameDto(Game game)
        {
            Id = game.Id;
            Name = game.Name;
            Master = new UserDto(game.Master);
            StartedAt = game.StartedAt;
            FinishedAt = game.FinishedAt;
            Items = new List<ItemDto>();
            foreach (var item in game.Items)
            {
                Items.Add(new ItemDto(item));
            }
        }
    }
}
