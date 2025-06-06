namespace Win3Thread
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonStartAll;
        private System.Windows.Forms.Button buttonStopAll;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonStartAll = new System.Windows.Forms.Button();
            this.buttonStopAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 200);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightYellow;
            this.panel2.Location = new System.Drawing.Point(330, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 200);
            this.panel2.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 230);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(618, 100);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // buttonStartAll
            // 
            this.buttonStartAll.Location = new System.Drawing.Point(150, 350);
            this.buttonStartAll.Name = "buttonStartAll";
            this.buttonStartAll.Size = new System.Drawing.Size(150, 40);
            this.buttonStartAll.TabIndex = 3;
            this.buttonStartAll.Text = "Start All";
            this.buttonStartAll.UseVisualStyleBackColor = true;
            this.buttonStartAll.Click += new System.EventHandler(this.buttonStartAll_Click);
            // 
            // buttonStopAll
            // 
            this.buttonStopAll.Location = new System.Drawing.Point(330, 350);
            this.buttonStopAll.Name = "buttonStopAll";
            this.buttonStopAll.Size = new System.Drawing.Size(150, 40);
            this.buttonStopAll.TabIndex = 4;
            this.buttonStopAll.Text = "Stop All";
            this.buttonStopAll.UseVisualStyleBackColor = true;
            this.buttonStopAll.Click += new System.EventHandler(this.buttonStopAll_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(644, 411);
            this.Controls.Add(this.buttonStopAll);
            this.Controls.Add(this.buttonStartAll);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Multithreaded Drawing App";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
        }
    }
}
