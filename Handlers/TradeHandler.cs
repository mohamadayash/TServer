using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingServer.Classes;
using TradingServer.DB;

namespace TradingServer.Handlers
{
    public static class TradeHandler
    {
        public static void Handle(ParsedCommand parsedCommand)
        {
            var userid = parsedCommand.Client.UserId;

            //parse command
            var login = long.Parse(parsedCommand.Parameters[0]);
            var deal = long.Parse(parsedCommand.Parameters[1]);
            var action = int.Parse(parsedCommand.Parameters[2]);
            var symbol = parsedCommand.Parameters[3];
            var price = double.Parse(parsedCommand.Parameters[4]);
            var profit = double.Parse(parsedCommand.Parameters[5]);
            var volume = long.Parse(parsedCommand.Parameters[6]);
            var time = long.Parse(parsedCommand.Parameters[7]);

            DateTime datetime = DateTimeOffset.FromUnixTimeSeconds(time).DateTime;

            DatabaseManager.LogTrade(userid, login, deal, action, symbol, price, profit, volume,datetime);

        }
    }
}
