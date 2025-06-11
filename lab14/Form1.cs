// Form1.cs
using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace FileSystemExplorer
{
    public partial class Form1 : Form
    {
        private object clipboardItem = null;
        private bool isCutOperation = false;

        public Form1()
        {
            InitializeComponent();
            textBoxFileFilter.Text = "";
            textBoxDirFilter.Text = "";
            LoadDrives();
            textBoxContent.ReadOnly = false;
        }

        private void LoadDrives()
        {
            treeViewDrives.Nodes.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    TreeNode node = new TreeNode(drive.Name) { Tag = drive };
                    node.Nodes.Add("Loading...");
                    treeViewDrives.Nodes.Add(node);
                }
            }
        }

        private void treeViewDrives_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            node.Nodes.Clear();
            try
            {
                DirectoryInfo dirInfo = node.Tag is DriveInfo drive ? drive.RootDirectory : new DirectoryInfo(node.Tag.ToString());
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    if ((dir.Attributes & FileAttributes.Hidden) == 0)
                    {
                        TreeNode subNode = new TreeNode(dir.Name) { Tag = dir.FullName };
                        subNode.Nodes.Add("Loading...");
                        node.Nodes.Add(subNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accessing directory: {ex.Message}");
            }
        }

        private void treeViewDrives_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listViewItems.Items.Clear();
            textBoxProperties.Clear();
            pictureBoxPreview.Image = null;
            textBoxContent.Clear();

            try
            {
                string path = e.Node.Tag is DriveInfo drive ? drive.RootDirectory.FullName : e.Node.Tag.ToString();
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (dirInfo.Exists)
                {
                    DisplayDirectoryProperties(dirInfo);
                    PopulateListView(dirInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accessing directory: {ex.Message}");
            }
        }

        private void PopulateListView(DirectoryInfo dirInfo)
        {
            listViewItems.Items.Clear();
            string dirFilter = textBoxDirFilter.Text.Trim().ToLower();
            string fileFilter = textBoxFileFilter.Text.Trim().ToLower();

            try
            {
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    if ((dir.Attributes & FileAttributes.Hidden) == 0 &&
                        (string.IsNullOrEmpty(dirFilter) || dir.Name.ToLower().Contains(dirFilter)))
                    {
                        ListViewItem item = new ListViewItem(dir.Name);
                        item.SubItems.Add("Directory");
                        item.SubItems.Add(dir.LastWriteTime.ToString());
                        item.Tag = dir;
                        listViewItems.Items.Add(item);
                    }
                }

                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    if ((file.Attributes & FileAttributes.Hidden) == 0 &&
                        (string.IsNullOrEmpty(fileFilter) || file.Name.ToLower().Contains(fileFilter)))
                    {
                        ListViewItem item = new ListViewItem(file.Name);
                        item.SubItems.Add(file.Extension);
                        item.SubItems.Add(file.LastWriteTime.ToString());
                        item.SubItems.Add((file.Length / 1024.0).ToString("N2") + " KB");
                        item.Tag = file;
                        listViewItems.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error listing items: {ex.Message}");
            }
        }

        private void DisplayDirectoryProperties(DirectoryInfo dirInfo)
        {
            textBoxProperties.Text = $"Directory Name: {dirInfo.Name}\r\n" +
                                     $"Full Path: {dirInfo.FullName}\r\n" +
                                     $"Creation Time: {dirInfo.CreationTime}\r\n" +
                                     $"Last Access: {dirInfo.LastAccessTime}\r\n" +
                                     $"Last Write: {dirInfo.LastWriteTime}\r\n" +
                                     $"Root: {dirInfo.Root}\r\n";

            try
            {
                DirectorySecurity security = dirInfo.GetAccessControl();
                foreach (FileSystemAccessRule rule in security.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
                {
                    textBoxProperties.Text += $"Access Rule: {rule.IdentityReference} - {rule.FileSystemRights} - {rule.AccessControlType}\r\n";
                }
            }
            catch (Exception ex)
            {
                textBoxProperties.Text += $"Security Info Error: {ex.Message}\r\n";
            }
        }

        private void listViewItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count > 0)
            {
                var item = listViewItems.SelectedItems[0];
                textBoxProperties.Clear();
                pictureBoxPreview.Image = null;
                textBoxContent.Clear();

                if (item.Tag is DirectoryInfo dirInfo)
                {
                    DisplayDirectoryProperties(dirInfo);
                }
                else if (item.Tag is FileInfo fileInfo)
                {
                    textBoxProperties.Text = $"File Name: {fileInfo.Name}\r\n" +
                                            $"Full Path: {fileInfo.FullName}\r\n" +
                                            $"Creation Time: {fileInfo.CreationTime}\r\n" +
                                            $"Last Access: {fileInfo.LastAccessTime}\r\n" +
                                            $"Last Write: {fileInfo.LastWriteTime}\r\n" +
                                            $"Size: {(fileInfo.Length / 1024.0).ToString("N2")} KB\r\n" +
                                            $"Extension: {fileInfo.Extension}\r\n";

                    try
                    {
                        FileSecurity security = fileInfo.GetAccessControl();
                        foreach (FileSystemAccessRule rule in security.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
                        {
                            textBoxProperties.Text += $"Access Rule: {rule.IdentityReference} - {rule.FileSystemRights} - {rule.AccessControlType}\r\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        textBoxProperties.Text += $"Security Info Error: {ex.Message}\r\n";
                    }

                    if (fileInfo.Extension.ToLower().Contains("txt"))
                    {
                        try { textBoxContent.Text = File.ReadAllText(fileInfo.FullName); }
                        catch (Exception ex) { textBoxContent.Text = $"Error reading text file: {ex.Message}"; }
                    }
                    else if (new[] { ".jpg", ".jpeg", ".png", ".bmp" }.Contains(fileInfo.Extension.ToLower()))
                    {
                        try { pictureBoxPreview.Image = Image.FromFile(fileInfo.FullName); }
                        catch (Exception ex) { pictureBoxPreview.Image = null; textBoxContent.Text = $"Error loading image: {ex.Message}"; }
                    }
                }
            }
        }

        private void textBoxDirFilter_TextChanged(object sender, EventArgs e)
        {
            if (treeViewDrives.SelectedNode != null)
            {
                string path = treeViewDrives.SelectedNode.Tag is DriveInfo drive ? drive.RootDirectory.FullName : treeViewDrives.SelectedNode.Tag.ToString();
                PopulateListView(new DirectoryInfo(path));
            }
        }

        private void textBoxFileFilter_TextChanged(object sender, EventArgs e)
        {
            if (treeViewDrives.SelectedNode != null)
            {
                string path = treeViewDrives.SelectedNode.Tag is DriveInfo drive ? drive.RootDirectory.FullName : treeViewDrives.SelectedNode.Tag.ToString();
                PopulateListView(new DirectoryInfo(path));
            }
        }

        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            Directory.CreateDirectory(destinationDir);
            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string dest = Path.Combine(destinationDir, Path.GetFileName(file));
                File.Copy(file, dest, true);
            }
            foreach (string dir in Directory.GetDirectories(sourceDir))
            {
                string dest = Path.Combine(destinationDir, Path.GetFileName(dir));
                CopyDirectory(dir, dest);
            }
        }

        private void toolStripMenuItemCreateFile_Click(object sender, EventArgs e)
        {
            if (treeViewDrives.SelectedNode == null) return;
            string path = treeViewDrives.SelectedNode.Tag is DriveInfo drive ? drive.RootDirectory.FullName : treeViewDrives.SelectedNode.Tag.ToString();
            string newFilePath = Path.Combine(path, "newfile.txt");
            File.WriteAllText(newFilePath, "");
            PopulateListView(new DirectoryInfo(path));
        }

        private void toolStripMenuItemCreateFolder_Click(object sender, EventArgs e)
        {
            if (treeViewDrives.SelectedNode == null) return;
            string path = treeViewDrives.SelectedNode.Tag is DriveInfo drive ? drive.RootDirectory.FullName : treeViewDrives.SelectedNode.Tag.ToString();
            string newDir = Path.Combine(path, "NewFolder_" + DateTime.Now.Ticks);
            Directory.CreateDirectory(newDir);
            PopulateListView(new DirectoryInfo(path));
        }

        private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count == 0) return;
            var item = listViewItems.SelectedItems[0];
            if (item.Tag is FileInfo fi) fi.Delete();
            else if (item.Tag is DirectoryInfo di) di.Delete(true);
            treeViewDrives_AfterSelect(null, new TreeViewEventArgs(treeViewDrives.SelectedNode));
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count == 0) return;
            clipboardItem = listViewItems.SelectedItems[0].Tag;
            isCutOperation = false;
        }

        private void toolStripMenuItemMove_Click(object sender, EventArgs e)
        {
            if (clipboardItem == null || treeViewDrives.SelectedNode == null) return;

            string targetPath = treeViewDrives.SelectedNode.Tag is DriveInfo d
                ? d.RootDirectory.FullName
                : treeViewDrives.SelectedNode.Tag.ToString();

            try
            {
                if (clipboardItem is FileInfo fi)
                {
                    string dest = Path.Combine(targetPath, fi.Name);
                    if (isCutOperation) fi.MoveTo(dest); else fi.CopyTo(dest, true);
                }
                else if (clipboardItem is DirectoryInfo di)
                {
                    string dest = Path.Combine(targetPath, di.Name);
                    if (isCutOperation) di.MoveTo(dest); else CopyDirectory(di.FullName, dest);
                }
                clipboardItem = null;
                isCutOperation = false;
                PopulateListView(new DirectoryInfo(targetPath));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }
        private void toolStripMenuItemSaveAsText_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                FileName = "newfile.txt"
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(dlg.FileName, textBoxContent.Text);
                    MessageBox.Show("Файл збережено як: " + dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка збереження: " + ex.Message);
                }
            }
        }

        private void toolStripMenuItemArchive_Click(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count == 0) return;
            var item = listViewItems.SelectedItems[0];
            SaveFileDialog dlg = new SaveFileDialog { Filter = "ZIP files (*.zip)|*.zip" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string zipPath = dlg.FileName;
                if (item.Tag is FileInfo fi)
                {
                    using (var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                        archive.CreateEntryFromFile(fi.FullName, fi.Name);
                }
                else if (item.Tag is DirectoryInfo di)
                {
                    ZipFile.CreateFromDirectory(di.FullName, zipPath);
                }
            }
        }

        private void toolStripMenuItemExtract_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "ZIP files (*.zip)|*.zip" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string zipPath = dlg.FileName;
                string extractTo = Path.Combine(Path.GetDirectoryName(zipPath), Path.GetFileNameWithoutExtension(zipPath) + "_unzipped");
                ZipFile.ExtractToDirectory(zipPath, extractTo);
                MessageBox.Show("Розпаковано до: " + extractTo);
            }
        }

        private void toolStripMenuItemSaveText_Click(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count == 0) return;
            var item = listViewItems.SelectedItems[0];
            if (item.Tag is FileInfo fi && fi.Extension.ToLower().Contains("txt"))
            {
                File.WriteAllText(fi.FullName, textBoxContent.Text);
                MessageBox.Show("Файл збережено");
            }
        }
    }
}
