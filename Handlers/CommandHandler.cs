using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingServer.Classes;
using TradingServer.Enums;

namespace TradingServer.Handlers
{
    public static class CommandHandler
    {
        public static void Handle(ParsedCommand parsedCommand)
        {
            switch (parsedCommand.CommandType)
            {
                case EnumCommandType.LOGIN:
                    LoginHandler.Handle(parsedCommand);
                    break;
                case EnumCommandType.TRADE:
                    TradeHandler.Handle(parsedCommand);
                    break;
                default:
                    Console.WriteLine($"not supported command:{parsedCommand.CommandType}");
                    break;
            }
        }
    }
}
