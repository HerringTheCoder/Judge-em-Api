using System.Collections.Generic;
using System.Threading.Tasks;
using Storage.Tables;

namespace WebApi.Hubs
{
    public interface IGameClient
    {
        Task RefreshItemIndex(int itemId);
        Task RefreshItemList(IEnumerable<Item> items, string message="");
        Task RefreshVotingProgress(int voteCounter, int maxVotes);
        Task SendMessage(string message);
        Task DisbandGame(string message);
        Task ShowSummary(Summary summary);
    }
}
