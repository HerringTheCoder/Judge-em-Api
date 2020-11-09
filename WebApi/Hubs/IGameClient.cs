using System.Collections.Generic;
using System.Threading.Tasks;
using Storage.Tables;

namespace WebApi.Hubs
{
    public interface IGameClient
    {
        Task RefreshCurrentItemId(int itemId);
        Task RefreshItemList(IEnumerable<Item> items, string message="");
        Task RefreshPlayersList(IEnumerable<string> players);
        Task RefreshVotingProgress(int voteCounter, int maxVotes);
        Task RefreshCategories(IEnumerable<Category> categories);
        Task AllowGameControl(int userId);
        Task DisbandGame(string message);
        Task ShowSummary(Summary summary);
        Task SendPlayerProfileId(string profileId);
        Task SendMessage(string message, MessageType messageType = MessageType.Undefined);
    }
}
