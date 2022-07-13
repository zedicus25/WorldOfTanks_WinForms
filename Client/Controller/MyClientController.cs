using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    public class MyClientController
    {
        TcpClient client;
        NetworkStream stream;
        readonly int PORT = 8008;
        readonly string HOST = "127.0.0.1";
        public MyClientController()
        {
            client = new TcpClient();
            Connect();
        }

        private void Connect()
        {
            try
            {
                client.Connect(HOST, PORT);
                stream = client.GetStream();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
        private void Disconnect()
        {
            stream.Close();
            client.Close();
        }
        ~MyClientController()
        {
            Disconnect();
        }
    }
}
