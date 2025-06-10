using System;
using System.IO;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Drawing;
using System.Linq;

namespace FileSystemExplorer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxFileFilter.Text = ""; // Clear file filter on startup
            textBoxDirFilter.Text = ""; // Clear directory filter on startup
            LoadDrives();
        }

        private void LoadDrives()
        {
            treeViewDrives.Nodes.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    TreeNode node = new TreeNode(drive.Name);
                    node.Tag = drive;
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
                DirectoryInfo dirInfo = node.Tag is DriveInfo ? ((DriveInfo)node.Tag).RootDirectory : new DirectoryInfo((string)node.Tag);
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    if ((dir.Attributes & FileAttributes.Hidden) == 0)
                    {
                        TreeNode subNode = new TreeNode(dir.Name);
                        subNode.Tag = dir.FullName;
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
                string path = e.Node.Tag is DriveInfo ? ((DriveInfo)e.Node.Tag).RootDirectory.FullName : (string)e.Node.Tag;
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
                // Populate directories
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

                // Populate files
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    if ((file.Attributes & FileAttributes.Hidden) == 0 &&
                        (string.IsNullOrEmpty(fileFilter) || file.Name.ToLower().Contains(fileFilter)))
                    {
                        // Debug: Log file to console to verify enumeration
                        Console.WriteLine($"Found file: {file.Name}");
                        ListViewItem item = new ListViewItem(file.Name);
                        item.SubItems.Add(file.Extension);
                        item.SubItems.Add(file.LastWriteTime.ToString());
                        item.SubItems.Add((file.Length / 1024.0).ToString("N2") + " KB");
                        item.Tag = file;
                        listViewItems.Items.Add(item);
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Access denied to some items: {ex.Message}");
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
                        try
                        {
                            textBoxContent.Text = File.ReadAllText(fileInfo.FullName);
                        }
                        catch (Exception ex)
                        {
                            textBoxContent.Text = $"Error reading text file: {ex.Message}";
                        }
                    }
                    else if (new[] { ".jpg", ".jpeg", ".png", ".bmp" }.Contains(fileInfo.Extension.ToLower()))
                    {
                        try
                        {
                            pictureBoxPreview.Image = Image.FromFile(fileInfo.FullName);
                        }
                        catch (Exception ex)
                        {
                            pictureBoxPreview.Image = null;
                            textBoxContent.Text = $"Error loading image: {ex.Message}";
                        }
                    }
                }
            }
        }

        private void textBoxDirFilter_TextChanged(object sender, EventArgs e)
        {
            if (treeViewDrives.SelectedNode != null)
            {
                string path = treeViewDrives.SelectedNode.Tag is DriveInfo ? ((DriveInfo)treeViewDrives.SelectedNode.Tag).RootDirectory.FullName : (string)treeViewDrives.SelectedNode.Tag;
                PopulateListView(new DirectoryInfo(path));
            }
        }

        private void textBoxFileFilter_TextChanged(object sender, EventArgs e)
        {
            if (treeViewDrives.SelectedNode != null)
            {
                string path = treeViewDrives.SelectedNode.Tag is DriveInfo ? ((DriveInfo)treeViewDrives.SelectedNode.Tag).RootDirectory.FullName : (string)treeViewDrives.SelectedNode.Tag;
                PopulateListView(new DirectoryInfo(path));
            }
        }
    }
}