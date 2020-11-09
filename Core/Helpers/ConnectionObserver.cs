using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Helpers
{
    public static class ConnectionObserver
    {
        public static Dictionary<string, PlayerEntry> ConnectionStates { get; } = new Dictionary<string, PlayerEntry>();

        public static void CleanConnectionGroup(string groupName)
        {
            var keysToRemove = ConnectionStates
                .Where(entry => entry.Value.Group == groupName)
                .Select(entry => entry.Key);

            foreach (var key in keysToRemove)
            {
                ConnectionStates.Remove(key);
            }
        }

        public static List<string> GetPlayersList(string groupName)
        {
            return ConnectionStates
                .Where(entry => entry.Value.Group == groupName)
                .Select(entry => entry.Value.Nickname)
                .ToList();
        }

        public static string GetCurrentGroupName(string connectionId)
        {
            return ConnectionStates
                .Where(entry => entry.Key == connectionId)
                .Select(entry => entry.Value.Group)
                .FirstOrDefault();
        }
    }
}
