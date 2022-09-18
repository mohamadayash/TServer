using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingServer.Classes;
using TradingServer.DB;
using TradingServer.Enums;

namespace TradingServer.Handlers
{
    public static class LoginHandler
    {
        public static void Handle(ParsedCommand parsedCommand)
        {
            //parse parameters
            var userid = parsedCommand.Parameters[0];
            var password = parsedCommand.Parameters[1];
            //login
            var dbUserId = DatabaseManager.Login(userid, password);
            if (dbUserId != null)
            {
                //assign userid to the client
                parsedCommand.Client.UserId = dbUserId;
                //add to authenticated clients
                AuthenticatedClients.AddClient(dbUserId,parsedCommand.Client);
                //send login successfull
                var msgParts = new string[]
                {
                    EnumCommandType.LOGIN,
                    "1"
                };
                var command = string.Join(" ",msgParts);
                parsedCommand.Client.Send(command);
            }
            else
            {
                //send login failed
                var msgParts = new string[]
                {
                    EnumCommandType.LOGIN,
                    "0"
                };
                var command = string.Join(" ", msgParts);
                parsedCommand.Client.Send(command);
                //disconnect client
                parsedCommand.Client.Close();
            }
        }
    }
}
