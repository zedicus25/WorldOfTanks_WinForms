using Server.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class MainForm : Form
    {
        MyServer server;
        Task ListenTask;
        public MainForm()
        {
            InitializeComponent();
            server = new MyServer();
            server.ConnectClient += Server_ConnectClient;
            server.DisconnectClient += Server_DisconnectClient;
            ListenTask = new Task(server.Listen);
            ListenTask.Start();

        }

        private void Server_DisconnectClient(MyClient obj)
        {
            listBox1.Invoke((MethodInvoker)delegate
            {
                listBox1.Items.Remove(obj);
            });
        }

        private void Server_ConnectClient(MyClient obj)
        {
            listBox1.Invoke((MethodInvoker)delegate
            {
                listBox1.Items.Add(obj);
            });
           
        }
    }
}
