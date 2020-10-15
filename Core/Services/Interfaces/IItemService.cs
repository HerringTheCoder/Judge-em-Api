using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Requests;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface IItemService
    {
        public Task Add(ItemCreateRequest request);
        public Task Remove(int id);
        public Task<IEnumerable<Item>> GetItemsByGameId(int gameId);
    } 
}
