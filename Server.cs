using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TradingServer
{
    public class Server
    {
        TcpListener tcpListener;
     
 
        public void Start()
        {
            tcpListener = new TcpListener(IPAddress.Any, 5555);
            tcpListener.Start();

            Thread thread = new Thread(() => ListenToConnections());
            thread.Name = "ListenToConnection";
            thread.IsBackground = true;
            thread.Start();
        }

        private void ListenToConnections()
        {
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();

                var clientWrapper = new Client(client);
            }
        }


    }
}
