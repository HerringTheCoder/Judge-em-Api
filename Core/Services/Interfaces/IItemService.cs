using System.Collections.Generic;
using Core.Requests;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface IItemService
    {
        public void Add(ItemCreateRequest request);
        public void Remove(int id);
        public IEnumerable<Item> GetItemsByGameId(int gameId);
    }
}
