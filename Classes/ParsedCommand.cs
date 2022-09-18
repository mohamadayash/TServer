using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingServer.Classes
{
    public class ParsedCommand
    {
        public ParsedCommand(string commandType,List<string> parameters,Client client)
        {
            CommandType = commandType;
            Parameters = parameters;
            Client = client;
        }

        public string CommandType { get; }
        public List<string> Parameters { get; }
        public Client Client { get; }
    }
}
