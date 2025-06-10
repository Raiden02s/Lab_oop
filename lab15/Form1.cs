using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FtpClientApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            FadList.Items.Clear();
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(tbHost.Text);
                request.Credentials = new NetworkCredential(tbUser.Text, tbPass.Text);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    while (!reader.EndOfStream)
                        FadList.Items.Add(reader.ReadLine());

                    MessageBox.Show(response.WelcomeMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка підключення: " + ex.Message);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Всі файли|*.*|png files|*.png|jpg files|*.jpg|gif files|*.gif|bmp files|*.bmp|exe files|*.exe|rar files|*.rar|zip files|*.zip|txt files|*.txt";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox10.Text = openFileDialog1.FileName;
                    string uploadUrl = tbHost.Text + tbUpload.Text + openFileDialog1.SafeFileName;

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                    request.Credentials = new NetworkCredential(tbUser.Text, tbPass.Text);
                    request.Method = WebRequestMethods.Ftp.UploadFile;

                    byte[] fileData = File.ReadAllBytes(textBox10.Text);
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(fileData, 0, fileData.Length);
                    }

                    MessageBox.Show(openFileDialog1.SafeFileName + " завантажено");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка завантаження: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Файл не обрано");
            }
        }

        private void tbnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string dirUrl = tbHost.Text + tbNewDir.Text;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(dirUrl);
                request.Credentials = new NetworkCredential(tbUser.Text, tbPass.Text);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    MessageBox.Show("Каталог " + tbNewDir.Text + " створено");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка створення каталогу: " + ex.Message);
            }
        }
    }
}
