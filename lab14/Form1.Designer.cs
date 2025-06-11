using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();

            this.treeViewDrives = new TreeView();
            this.listViewItems = new ListView();
            this.columnHeaderName = new ColumnHeader();
            this.columnHeaderType = new ColumnHeader();
            this.columnHeaderDate = new ColumnHeader();
            this.columnHeaderSize = new ColumnHeader();
            this.textBoxProperties = new TextBox();
            this.textBoxDirFilter = new TextBox();
            this.textBoxFileFilter = new TextBox();
            this.textBoxContent = new TextBox();
            this.labelDirFilter = new Label();
            this.labelFileFilter = new Label();
            this.labelProperties = new Label();
            this.labelPreview = new Label();
            this.pictureBoxPreview = new PictureBox();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.toolStripMenuItemCreateFile = new ToolStripMenuItem();
            this.toolStripMenuItemCreateFolder = new ToolStripMenuItem();
            this.toolStripMenuItemDelete = new ToolStripMenuItem();
            this.toolStripMenuItemCopy = new ToolStripMenuItem();
            this.toolStripMenuItemMove = new ToolStripMenuItem();
            this.toolStripMenuItemArchive = new ToolStripMenuItem();
            this.toolStripMenuItemExtract = new ToolStripMenuItem();
            this.toolStripMenuItemSaveText = new ToolStripMenuItem();
            this.toolStripMenuItemSaveAsText = new ToolStripMenuItem();
            this.buttonSaveText = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();

            // TreeView
            this.treeViewDrives.Location = new System.Drawing.Point(12, 12);
            this.treeViewDrives.Size = new System.Drawing.Size(200, 600);
            this.treeViewDrives.BeforeExpand += new TreeViewCancelEventHandler(this.treeViewDrives_BeforeExpand);
            this.treeViewDrives.AfterSelect += new TreeViewEventHandler(this.treeViewDrives_AfterSelect);

            // ListView
            this.listViewItems.Columns.AddRange(new ColumnHeader[] {
                this.columnHeaderName, this.columnHeaderType, this.columnHeaderDate, this.columnHeaderSize });
            this.listViewItems.Location = new System.Drawing.Point(218, 40);
            this.listViewItems.Size = new System.Drawing.Size(550, 300);
            this.listViewItems.View = View.Details;
            this.listViewItems.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewItems.SelectedIndexChanged += new System.EventHandler(this.listViewItems_SelectedIndexChanged);

            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 200;
            this.columnHeaderType.Text = "Type";
            this.columnHeaderType.Width = 100;
            this.columnHeaderDate.Text = "Date Modified";
            this.columnHeaderDate.Width = 150;
            this.columnHeaderSize.Text = "Size";
            this.columnHeaderSize.Width = 100;

            // Filters
            this.labelDirFilter.Location = new System.Drawing.Point(218, 12);
            this.labelDirFilter.Size = new System.Drawing.Size(80, 23);
            this.labelDirFilter.Text = "Directory Filter";
            this.textBoxDirFilter.Location = new System.Drawing.Point(300, 12);
            this.textBoxDirFilter.Size = new System.Drawing.Size(150, 23);
            this.textBoxDirFilter.TextChanged += new System.EventHandler(this.textBoxDirFilter_TextChanged);

            this.labelFileFilter.Location = new System.Drawing.Point(528, 12);
            this.labelFileFilter.Size = new System.Drawing.Size(80, 23);
            this.labelFileFilter.Text = "File Filter";
            this.textBoxFileFilter.Location = new System.Drawing.Point(610, 12);
            this.textBoxFileFilter.Size = new System.Drawing.Size(150, 23);
            this.textBoxFileFilter.TextChanged += new System.EventHandler(this.textBoxFileFilter_TextChanged);

            // Properties box
            this.labelProperties.Location = new System.Drawing.Point(218, 346);
            this.labelProperties.Size = new System.Drawing.Size(100, 23);
            this.labelProperties.Text = "Properties";
            this.textBoxProperties.Location = new System.Drawing.Point(218, 370);
            this.textBoxProperties.Multiline = true;
            this.textBoxProperties.ReadOnly = true;
            this.textBoxProperties.Size = new System.Drawing.Size(550, 200);

            // Text file editor
            this.labelPreview.Location = new System.Drawing.Point(774, 346);
            this.labelPreview.Size = new System.Drawing.Size(100, 23);
            this.labelPreview.Text = "Preview";
            this.textBoxContent.Location = new System.Drawing.Point(774, 370);
            this.textBoxContent.Multiline = true;
            this.textBoxContent.ScrollBars = ScrollBars.Both;
            this.textBoxContent.Size = new System.Drawing.Size(300, 200);

            // Save Button
            this.buttonSaveText.Text = "Зберегти";
            this.buttonSaveText.Location = new System.Drawing.Point(774, 580);
            this.buttonSaveText.Size = new System.Drawing.Size(300, 30);
            this.buttonSaveText.Click += new System.EventHandler(this.toolStripMenuItemSaveText_Click);

            // PictureBox for image preview
            this.pictureBoxPreview.Location = new System.Drawing.Point(774, 40);
            this.pictureBoxPreview.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxPreview.SizeMode = PictureBoxSizeMode.Zoom;

            // Context menu
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] {
                this.toolStripMenuItemCreateFile,
                this.toolStripMenuItemCreateFolder,
                this.toolStripMenuItemDelete,
                this.toolStripMenuItemCopy,
                this.toolStripMenuItemMove,
                this.toolStripMenuItemArchive,
                this.toolStripMenuItemExtract,
                this.toolStripMenuItemSaveText,
                this.toolStripMenuItemSaveAsText });

            this.toolStripMenuItemCreateFile.Text = "Створити файл";
            this.toolStripMenuItemCreateFolder.Text = "Створити папку";
            this.toolStripMenuItemDelete.Text = "Видалити";
            this.toolStripMenuItemCopy.Text = "Копіювати";
            this.toolStripMenuItemMove.Text = "Перемістити";
            this.toolStripMenuItemArchive.Text = "Архівувати в ZIP";
            this.toolStripMenuItemExtract.Text = "Розпакувати ZIP";
            this.toolStripMenuItemSaveText.Text = "Зберегти текст";
            this.toolStripMenuItemSaveAsText.Text = "Зберегти як...";

            this.toolStripMenuItemCreateFile.Click += new System.EventHandler(this.toolStripMenuItemCreateFile_Click);
            this.toolStripMenuItemCreateFolder.Click += new System.EventHandler(this.toolStripMenuItemCreateFolder_Click);
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
            this.toolStripMenuItemMove.Click += new System.EventHandler(this.toolStripMenuItemMove_Click);
            this.toolStripMenuItemArchive.Click += new System.EventHandler(this.toolStripMenuItemArchive_Click);
            this.toolStripMenuItemExtract.Click += new System.EventHandler(this.toolStripMenuItemExtract_Click);
            this.toolStripMenuItemSaveText.Click += new System.EventHandler(this.toolStripMenuItemSaveText_Click);
            this.toolStripMenuItemSaveAsText.Click += new System.EventHandler(this.toolStripMenuItemSaveAsText_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(1090, 624);
            this.Controls.Add(this.treeViewDrives);
            this.Controls.Add(this.listViewItems);
            this.Controls.Add(this.textBoxDirFilter);
            this.Controls.Add(this.labelDirFilter);
            this.Controls.Add(this.textBoxFileFilter);
            this.Controls.Add(this.labelFileFilter);
            this.Controls.Add(this.textBoxProperties);
            this.Controls.Add(this.labelProperties);
            this.Controls.Add(this.pictureBoxPreview);
            this.Controls.Add(this.textBoxContent);
            this.Controls.Add(this.labelPreview);
            this.Controls.Add(this.buttonSaveText);
            this.Text = "File System Explorer";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
        }

        private TreeView treeViewDrives;
        private ListView listViewItems;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderType;
        private ColumnHeader columnHeaderDate;
        private ColumnHeader columnHeaderSize;
        private TextBox textBoxDirFilter;
        private TextBox textBoxFileFilter;
        private TextBox textBoxProperties;
        private TextBox textBoxContent;
        private PictureBox pictureBoxPreview;
        private Label labelDirFilter;
        private Label labelFileFilter;
        private Label labelProperties;
        private Label labelPreview;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItemCreateFile;
        private ToolStripMenuItem toolStripMenuItemCreateFolder;
        private ToolStripMenuItem toolStripMenuItemDelete;
        private ToolStripMenuItem toolStripMenuItemCopy;
        private ToolStripMenuItem toolStripMenuItemMove;
        private ToolStripMenuItem toolStripMenuItemArchive;
        private ToolStripMenuItem toolStripMenuItemExtract;
        private ToolStripMenuItem toolStripMenuItemSaveText;
        private ToolStripMenuItem toolStripMenuItemSaveAsText;
        private Button buttonSaveText;
    }
}
