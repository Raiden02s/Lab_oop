namespace FileSystemExplorer
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.treeViewDrives = new System.Windows.Forms.TreeView();
            this.listViewItems = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSize = new System.Windows.Forms.ColumnHeader();
            this.textBoxProperties = new System.Windows.Forms.TextBox();
            this.labelProperties = new System.Windows.Forms.Label();
            this.textBoxDirFilter = new System.Windows.Forms.TextBox();
            this.labelDirFilter = new System.Windows.Forms.Label();
            this.textBoxFileFilter = new System.Windows.Forms.TextBox();
            this.labelFileFilter = new System.Windows.Forms.Label();
            this.pictureBoxPreview = new System.Windows.Forms.PictureBox();
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.labelPreview = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
            this.SuspendLayout();

            // treeViewDrives
            this.treeViewDrives.Location = new System.Drawing.Point(12, 12);
            this.treeViewDrives.Size = new System.Drawing.Size(200, 600);
            this.treeViewDrives.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewDrives_BeforeExpand);
            this.treeViewDrives.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDrives_AfterSelect);

            // listViewItems
            this.listViewItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.columnHeaderName,
                this.columnHeaderType,
                this.columnHeaderDate,
                this.columnHeaderSize});
            this.listViewItems.Location = new System.Drawing.Point(218, 40);
            this.listViewItems.Size = new System.Drawing.Size(550, 300);
            this.listViewItems.View = System.Windows.Forms.View.Details;
            this.listViewItems.SelectedIndexChanged += new System.EventHandler(this.listViewItems_SelectedIndexChanged);

            // columnHeaderName
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 200;

            // columnHeaderType
            this.columnHeaderType.Text = "Type";
            this.columnHeaderType.Width = 100;

            // columnHeaderDate
            this.columnHeaderDate.Text = "Date Modified";
            this.columnHeaderDate.Width = 150;

            // columnHeaderSize
            this.columnHeaderSize.Text = "Size";
            this.columnHeaderSize.Width = 100;

            // textBoxProperties
            this.textBoxProperties.Location = new System.Drawing.Point(218, 370);
            this.textBoxProperties.Multiline = true;
            this.textBoxProperties.Size = new System.Drawing.Size(550, 200);
            this.textBoxProperties.ReadOnly = true;

            // labelProperties
            this.labelProperties.Location = new System.Drawing.Point(218, 346);
            this.labelProperties.Size = new System.Drawing.Size(100, 23);
            this.labelProperties.Text = "Properties";

            // textBoxDirFilter
            this.textBoxDirFilter.Location = new System.Drawing.Point(300, 12);
            this.textBoxDirFilter.Size = new System.Drawing.Size(150, 23);
            this.textBoxDirFilter.TextChanged += new System.EventHandler(this.textBoxDirFilter_TextChanged);

            // labelDirFilter
            this.labelDirFilter.Location = new System.Drawing.Point(218, 12);
            this.labelDirFilter.Size = new System.Drawing.Size(80, 23);
            this.labelDirFilter.Text = "Directory Filter";

            // textBoxFileFilter
            this.textBoxFileFilter.Location = new System.Drawing.Point(610, 12);
            this.textBoxFileFilter.Size = new System.Drawing.Size(150, 23);
            this.textBoxFileFilter.TextChanged += new System.EventHandler(this.textBoxFileFilter_TextChanged);

            // labelFileFilter
            this.labelFileFilter.Location = new System.Drawing.Point(528, 12);
            this.labelFileFilter.Size = new System.Drawing.Size(80, 23);
            this.labelFileFilter.Text = "File Filter";

            // pictureBoxPreview
            this.pictureBoxPreview.Location = new System.Drawing.Point(774, 40);
            this.pictureBoxPreview.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // textBoxContent
            this.textBoxContent.Location = new System.Drawing.Point(774, 370);
            this.textBoxContent.Multiline = true;
            this.textBoxContent.Size = new System.Drawing.Size(300, 200);
            this.textBoxContent.ReadOnly = true;

            // labelPreview
            this.labelPreview.Location = new System.Drawing.Point(774, 346);
            this.labelPreview.Size = new System.Drawing.Size(100, 23);
            this.labelPreview.Text = "Preview";

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 624);
            this.Controls.Add(this.treeViewDrives);
            this.Controls.Add(this.listViewItems);
            this.Controls.Add(this.textBoxProperties);
            this.Controls.Add(this.labelProperties);
            this.Controls.Add(this.textBoxDirFilter);
            this.Controls.Add(this.labelDirFilter);
            this.Controls.Add(this.textBoxFileFilter);
            this.Controls.Add(this.labelFileFilter);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.textBoxContent);
            this.Controls.Add(this.labelPreview);
            this.Text = "File System Explorer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TreeView treeViewDrives;
        private System.Windows.Forms.ListView listViewItems;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderType;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.TextBox textBoxProperties;
        private System.Windows.Forms.Label labelProperties;
        private System.Windows.Forms.TextBox textBoxDirFilter;
        private System.Windows.Forms.Label labelDirFilter;
        private System.Windows.Forms.TextBox textBoxFileFilter;
        private System.Windows.Forms.Label labelFileFilter;
        private System.Windows.Forms.PictureBox pictureBoxPreview;
        private System.Windows.Forms.TextBox textBoxContent;
        private System.Windows.Forms.Label labelPreview;
    }
}