using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;

namespace UdpChat
{
    public partial class Form1 : Form
    {
        bool alive = false; // Controls receive thread
        UdpClient client;
        private int LOCALPORT = 8001; // Port for receiving messages
        private int REMOTEPORT = 8001; // Port for sending messages
        private int TTL = 20;
        private string HOST = "235.5.5.1"; // Multicast group address
        IPAddress groupAddress; // Address for multicast
        string userName; // User name in chat
        string logFilePath; // Path for chat log, set via Save As dialog

        public Form1()
        {
            InitializeComponent();
            loginButton.Enabled = true;
            logoutButton.Enabled = false;
            sendButton.Enabled = false;
            saveLogButton.Enabled = false; // Disabled until user logs in
            chatTextBox.ReadOnly = true;
            groupAddress = IPAddress.Parse(HOST);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            userName = userNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Please enter a username");
                return;
            }
            userNameTextBox.ReadOnly = true;
            try
            {
                client = new UdpClient(LOCALPORT);
                client.JoinMulticastGroup(groupAddress, TTL);
                Task receiveTask = new Task(ReceiveMessages);
                receiveTask.Start();
                string message = userName + " вошел в чат";
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, HOST, REMOTEPORT);
                LogMessage(message);
                loginButton.Enabled = false;
                logoutButton.Enabled = true;
                sendButton.Enabled = true;
                saveLogButton.Enabled = true; // Enable Save Log button after login
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReceiveMessages()
        {
            alive = true;
            try
            {
                while (alive)
                {
                    IPEndPoint remoteIp = null;
                    byte[] data = client.Receive(ref remoteIp);
                    string message = Encoding.Unicode.GetString(data);
                    this.Invoke(new MethodInvoker(() =>
                    {
                        string time = DateTime.Now.ToShortTimeString();
                        chatTextBox.Text = time + " " + message + "\r\n" + chatTextBox.Text;
                        LogMessage(message);
                    }));
                }
            }
            catch (ObjectDisposedException)
            {
                if (!alive)
                    return;
                throw;
            }
            catch (Exception ex)
            {
                if (alive)
                    MessageBox.Show(ex.Message);
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            try
            {
                string message = String.Format("{0}: {1}", userName, messageTextBox.Text);
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, HOST, REMOTEPORT);
                LogMessage(message);
                messageTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            ExitChat();
        }

        private void ExitChat()
        {
            if (client != null)
            {
                try
                {
                    string message = userName + " покидает чат";
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    client.Send(data, data.Length, HOST, REMOTEPORT);
                    LogMessage(message);
                    alive = false;
                    client.DropMulticastGroup(groupAddress);
                    client.Close();
                    client = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            loginButton.Enabled = true;
            logoutButton.Enabled = false;
            sendButton.Enabled = false;
            saveLogButton.Enabled = false;
            userNameTextBox.ReadOnly = false;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new Form())
            {
                settingsForm.Text = "Chat Settings";
                settingsForm.Size = new System.Drawing.Size(300, 200);

                var hostLabel = new Label() { Text = "Host:", Location = new System.Drawing.Point(10, 20), AutoSize = true };
                var hostTextBox = new TextBox() { Text = HOST, Location = new System.Drawing.Point(80, 20), Width = 150 };

                var localPortLabel = new Label() { Text = "Local Port:", Location = new System.Drawing.Point(10, 50), AutoSize = true };
                var localPortTextBox = new TextBox() { Text = LOCALPORT.ToString(), Location = new System.Drawing.Point(80, 50), Width = 150 };

                var remotePortLabel = new Label() { Text = "Remote Port:", Location = new System.Drawing.Point(10, 80), AutoSize = true };
                var remotePortTextBox = new TextBox() { Text = REMOTEPORT.ToString(), Location = new System.Drawing.Point(80, 80), Width = 150 };

                var fontButton = new Button() { Text = "Change Font", Location = new System.Drawing.Point(80, 110), Width = 100 };
                fontButton.Click += (s, ev) =>
                {
                    using (var fontDialog = new FontDialog())
                    {
                        if (fontDialog.ShowDialog() == DialogResult.OK)
                        {
                            chatTextBox.Font = fontDialog.Font;
                        }
                    }
                };

                var saveButton = new Button() { Text = "Save", Location = new System.Drawing.Point(80, 140), Width = 75 };
                saveButton.Click += (s, ev) =>
                {
                    try
                    {
                        HOST = hostTextBox.Text;
                        groupAddress = IPAddress.Parse(HOST);
                        LOCALPORT = int.Parse(localPortTextBox.Text);
                        REMOTEPORT = int.Parse(remotePortTextBox.Text);
                        settingsForm.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid settings: " + ex.Message);
                    }
                };

                settingsForm.Controls.Add(hostLabel);
                settingsForm.Controls.Add(hostTextBox);
                settingsForm.Controls.Add(localPortLabel);
                settingsForm.Controls.Add(localPortTextBox);
                settingsForm.Controls.Add(remotePortLabel);
                settingsForm.Controls.Add(remotePortTextBox);
                settingsForm.Controls.Add(fontButton);
                settingsForm.Controls.Add(saveButton);

                settingsForm.ShowDialog();
            }
        }

        private void saveLogButton_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Save Chat Log";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.FileName = "chat_log.txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    logFilePath = saveFileDialog.FileName;
                    try
                    {
                        // Write current chatTextBox content to the file if it doesn't exist yet
                        if (!File.Exists(logFilePath))
                        {
                            File.WriteAllText(logFilePath, chatTextBox.Text);
                        }
                        MessageBox.Show($"Chat log will be saved to: {logFilePath}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error setting log file: " + ex.Message);
                    }
                }
            }
        }

        private void LogMessage(string message)
        {
            if (string.IsNullOrEmpty(logFilePath))
                return; // Skip logging if no file is selected

            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                File.AppendAllText(logFilePath, $"[{time}] {message}\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing to log file: " + ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExitChat();
        }
    }
}