using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradingServer.Enums;
using TradingServer.Handlers;

namespace TradingServer
{
    public class Client
    {
        private readonly TcpClient client;
        private BlockingCollection<string> receiveQueue;
        private BlockingCollection<string> sendQueue;

        private StreamReader streamReader;
        private StreamWriter streamWriter;

        public string UserId { get; set; }

        public Client(TcpClient client)
        {
            this.client = client;

            streamReader = new StreamReader(client.GetStream());
            streamWriter = new StreamWriter(client.GetStream());

            receiveQueue = new BlockingCollection<string>();
            sendQueue = new BlockingCollection<string>();


            Thread threadConsumeReceive = new Thread(() => ConsumeReceive());
            threadConsumeReceive.IsBackground = true;
            threadConsumeReceive.Start();

            Thread threadConsumeSend = new Thread(() => ConsumeSend());
            threadConsumeSend.IsBackground = true;
            threadConsumeSend.Start();

            Thread thread = new Thread(() => Receive());
            thread.IsBackground = true;
            thread.Start();
        }

  

        public void Send(string command)
        {
            sendQueue.Add(command);
        }
        public void Close()
        {
            while (receiveQueue.Count > 0) ;

            this.client.Close();
            this.client.Dispose();

            this.streamReader.Dispose();
            this.streamWriter.Dispose();
        }

        private void Receive()
        {
            while (true)
            {
                try
                {
                    var command = streamReader.ReadLine();
                    Console.WriteLine("received command:" + command);
                    if (command == null)
                        break;
                    receiveQueue.Add(command);
                }catch(Exception ex)
                {
                    break;
                }
            }

            AuthenticatedClients.RemoveClient(this.UserId);
        }


        private void ConsumeReceive()
        {
            foreach(string command in receiveQueue.GetConsumingEnumerable())
            {
                var parsedCommand = CommandParser.Parse(this, command);
                CommandHandler.Handle(parsedCommand);
            }
        }

        private void ConsumeSend()
        {
            foreach (string command in sendQueue.GetConsumingEnumerable())
            {
                try
                {
                    this.streamWriter.WriteLine(command);
                    this.streamWriter.Flush();
                }
                catch (Exception)
                {
                }
            }
        }


    }
}
