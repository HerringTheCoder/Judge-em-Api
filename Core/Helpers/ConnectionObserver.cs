using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{
    public static class ConnectionObserver
    {
        public static Dictionary<string, string> ConnectionStates => new Dictionary<string,string>();

        public static void CleanConnectionGroup(string gameCode)
        {
            var keysToRemove = ConnectionStates
                .Where(entry => entry.Value == gameCode)
                .Select(entry => entry.Key)
                .ToList();

            foreach (var key in keysToRemove)
            {
                ConnectionStates.Remove(key);
            }
        }
    }
}
