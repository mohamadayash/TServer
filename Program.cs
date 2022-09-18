using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting trading server ...");

            Server server = new Server();
            server.Start();

            while (true) ;
        }
    }
}
