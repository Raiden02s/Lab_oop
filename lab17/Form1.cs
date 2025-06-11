using System;
using System.Windows.Forms;
using System.Threading;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        ServerObject server;
        Thread listenThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            server = new ServerObject();
            listenThread = new Thread(new ThreadStart(server.Listen));
            listenThread.Start();
            btnStart.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server?.Disconnect();
            listenThread?.Abort();
        }
    }
}
