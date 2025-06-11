using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        TcpClient client;
        NetworkStream stream;
        Thread receiveThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 8888);
                stream = client.GetStream();

                byte[] data = Encoding.Unicode.GetBytes(txtName.Text);
                stream.Write(data, 0, data.Length);

                receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();

                AppendText($"Підключено як {txtName.Text}\r\n");
                btnSend.Enabled = true;
                btnConnect.Enabled = false;
            }
            catch (Exception ex)
            {
                AppendText("Помилка підключення: " + ex.Message + "\r\n");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
{
    if (!string.IsNullOrWhiteSpace(txtMessage.Text))
    {
        string message = txtMessage.Text;

        // локальне відображення
        AppendText($"{txtName.Text}: {message}\r\n");

        byte[] data = Encoding.Unicode.GetBytes(message);
        stream.Write(data, 0, data.Length);

        txtMessage.Clear();
    }
}

private void ReceiveMessage()
{
    while (true)
    {
        try
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;

            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            string message = builder.ToString();
            AppendText(message + "\r\n");
        }
        catch (Exception ex)
        {
            AppendText("Помилка прийому: " + ex.Message + "\r\n");
            Disconnect();
            break;
        }
    }
}


        private void AppendText(string text)
        {
            if (txtChat.InvokeRequired)
                txtChat.Invoke(new Action<string>(AppendText), text);
            else
                txtChat.AppendText(text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void Disconnect()
        {
            stream?.Close();
            client?.Close();
            Environment.Exit(0);
        }
    }
}
