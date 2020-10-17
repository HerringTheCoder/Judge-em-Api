using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Requests;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface IItemService
    {
        Task Add(ItemCreateRequest request, int gameId);
        Task Remove(int id);
        Task<IEnumerable<Item>> GetItemsByGameId(int gameId);
    }
}
