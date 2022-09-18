using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TradingServer
{
    public static class AuthenticatedClients
    {
        private static ConcurrentDictionary<string,Client> authenticatedClients = new ConcurrentDictionary<string,Client>();
        public static void AddClient(string userid,Client client)
        {
            Console.WriteLine($"client userid:{userid} is added.");
            authenticatedClients.TryAdd(userid,client);
        }

        public static bool RemoveClient(string userid)
        {
            if (userid == null) return false;
            Console.WriteLine($"client userid:{userid} is removed.");
            return authenticatedClients.TryRemove(userid,out var client);
        }
    }
}
