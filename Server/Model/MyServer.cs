using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Model
{
    public class MyServer
    {
        private TcpListener tcpListener;
        private List<MyClient> clients;
        readonly int PORT;
        public event Action<MyClient> ConnectClient;
        public event Action<MyClient> DisconnectClient;

        public MyServer(int port = 8008)
        {
            clients = new List<MyClient>();
            this.PORT = port;
        }

        public void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, PORT);
                tcpListener.Start();
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    MyClient myClient = new MyClient(client, this);

                    Thread clientThread = new Thread(new ThreadStart(myClient.Work));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                CloseServer();
            }
        }

        public void BroadcastMsg(string msg, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(msg);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id != id)
                    clients[i].networkStream.Write(data, 0, data.Length);
            }
        }

        public void DeleteConnetion(string id)
        {
            MyClient client = clients.FirstOrDefault(x => x.Id.Equals(id));
            if (client != null)
            {
                clients.Remove(client);
                client.Close();
                DisconnectClient?.Invoke(client);
            }
        }

        public void AddConnection(MyClient myClient)
        {
            clients.Add(myClient);
            ConnectClient?.Invoke(clients.Last());
        }
        public void CloseServer()
        {
            tcpListener.Stop();
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close();
            }
        }

        public List<MyClient> GetClients() => clients;

        public void SendMessageToClient(string id, string msg)
        {
            MyClient cl = clients.FirstOrDefault(x => x.Id.Equals(id));
            cl?.networkStream.Write(Encoding.Unicode.GetBytes(msg), 0, Encoding.Unicode.GetBytes(msg).Length);
        }


        ~MyServer()
        {
            CloseServer();
        }
    }
}
