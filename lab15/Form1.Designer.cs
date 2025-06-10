namespace FtpClientApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.TextBox tbUpload;
        private System.Windows.Forms.TextBox tbNewDir;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button tbnCreate;
        private System.Windows.Forms.ListBox FadList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tbHost = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.tbUpload = new System.Windows.Forms.TextBox();
            this.tbNewDir = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.tbnCreate = new System.Windows.Forms.Button();
            this.FadList = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(12, 12);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(300, 20);
            this.tbHost.TabIndex = 0;
            this.tbHost.Text = "ftp://localhost/";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(12, 38);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(145, 20);
            this.tbUser.TabIndex = 1;
            this.tbUser.Text = "user";
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(167, 38);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(145, 20);
            this.tbPass.TabIndex = 2;
            this.tbPass.PasswordChar = '*';
            this.tbPass.Text = "password";
            // 
            // tbUpload
            // 
            this.tbUpload.Location = new System.Drawing.Point(12, 64);
            this.tbUpload.Name = "tbUpload";
            this.tbUpload.Size = new System.Drawing.Size(300, 20);
            this.tbUpload.TabIndex = 3;
            this.tbUpload.Text = "/";
            // 
            // tbNewDir
            // 
            this.tbNewDir.Location = new System.Drawing.Point(12, 90);
            this.tbNewDir.Name = "tbNewDir";
            this.tbNewDir.Size = new System.Drawing.Size(300, 20);
            this.tbNewDir.TabIndex = 4;
            this.tbNewDir.Text = "/newfolder/";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(12, 116);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(300, 20);
            this.textBox10.TabIndex = 5;
            this.textBox10.ReadOnly = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(318, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Підключитися";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(318, 64);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(100, 23);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "Завантажити";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // tbnCreate
            // 
            this.tbnCreate.Location = new System.Drawing.Point(318, 90);
            this.tbnCreate.Name = "tbnCreate";
            this.tbnCreate.Size = new System.Drawing.Size(100, 23);
            this.tbnCreate.TabIndex = 8;
            this.tbnCreate.Text = "Створити каталог";
            this.tbnCreate.UseVisualStyleBackColor = true;
            this.tbnCreate.Click += new System.EventHandler(this.tbnCreate_Click);
            // 
            // FadList
            // 
            this.FadList.FormattingEnabled = true;
            this.FadList.Location = new System.Drawing.Point(12, 150);
            this.FadList.Name = "FadList";
            this.FadList.Size = new System.Drawing.Size(406, 160);
            this.FadList.TabIndex = 9;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(430, 320);
            this.Controls.Add(this.FadList);
            this.Controls.Add(this.tbnCreate);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.tbNewDir);
            this.Controls.Add(this.tbUpload);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.tbHost);
            this.Name = "Form1";
            this.Text = "FTP Client";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
