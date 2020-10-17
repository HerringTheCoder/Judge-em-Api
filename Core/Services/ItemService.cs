using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Requests;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Core.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task Add(ItemCreateRequest request, int gameId)
        {
            var item = new Item
            {
            Name = request.Name,
            Description = request.Description,
            ImageLink = request.ImageLink,
            GameId = gameId
            };
            _itemRepository.Add(item);
            await _itemRepository.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var item = _itemRepository.Get(i => i.Id == id).FirstOrDefault();
            _itemRepository.Delete(item);
            await _itemRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsByGameId(int gameId)
        {
            return await _itemRepository.Get(g => g.GameId == gameId).ToListAsync();
        }
    }
}
