using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public class MyClient
    {
        private MyServer _server;

        public string Id { get; private set; }
        protected TcpClient tcpClient;
        public bool CanWrite { get; set; } = true;
        internal NetworkStream networkStream { get; set; }

        public MyClient(TcpClient tcpClient, MyServer myServer)
        {
            Id = Guid.NewGuid().ToString();
            _server = myServer;
            this.tcpClient = tcpClient;
            _server.AddConnection(this);
        }

        public void Work()
        {
            try
            {
                networkStream = tcpClient.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _server.DeleteConnetion(this.Id);
                Close();
            }
        }

        public string GetMsg()
        {
            byte[] data = new byte[256];
            StringBuilder builder = new StringBuilder();
            int byteCount = 0;
            do
            {
                byteCount = networkStream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, byteCount));
            } while (networkStream.DataAvailable);

            return builder.ToString();
        }

        public void Close()
        {
            tcpClient.Close();
            networkStream.Close();
        }

        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public override string ToString()
        {
            return Id;
        }
        ~MyClient()
        {
            Close();
        }
    }
}
