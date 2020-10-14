using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Requests;
using Core.Services.Interfaces;
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
        public void Add(ItemCreateRequest request)
        {
            var item = new Item
            {
            Name = request.Name,
            Description = request.Description,
            ImageLink = request.ImageLink
            };
            _itemRepository.Add(item);
            _itemRepository.SaveChanges();
        }

        public void Remove(int id)
        {
            var item = _itemRepository.Get(i => i.Id == id).FirstOrDefault();
            _itemRepository.Delete(item);
        }

        public IEnumerable<Item> GetItemsByGameId(int gameId)
        {
            var items = _itemRepository.GetAll().Where(i => i.Id == gameId);
            return items;
        }
    }
}
