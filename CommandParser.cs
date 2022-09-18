using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingServer.Classes;

namespace TradingServer
{
    
    public static class CommandParser
    {
        private const char delimiter = ' ';
        public static ParsedCommand Parse(Client client,string command)
        {
            var splittedCommand = command.Split(delimiter);

            var commandType = splittedCommand[0];
            var parameters = splittedCommand.Skip(1).ToList();
            return new ParsedCommand(commandType, parameters,client);
        }
    }
}
