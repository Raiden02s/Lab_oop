using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ProcessManagerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBoxProcesses.ContextMenuStrip = contextMenuStrip1; // Attach context menu to ListBox
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            listBoxProcesses.Items.Clear();
            foreach (Process process in Process.GetProcesses())
            {
                listBoxProcesses.Items.Add($"{process.Id} - {process.ProcessName}");
            }
        }

        private void listBoxProcesses_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBoxProcesses.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    listBoxProcesses.SelectedIndex = index;
                    contextMenuStrip1.Show(listBoxProcesses, e.Location);
                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxProcesses.SelectedIndex != -1)
            {
                string selectedItem = listBoxProcesses.SelectedItem.ToString();
                int processId = int.Parse(selectedItem.Split('-')[0].Trim());
                try
                {
                    Process process = Process.GetProcessById(processId);

                    string details = $"Process Details:\n" +
                                    $"ID: {process.Id}\n" +
                                    $"Name: {process.ProcessName}\n" +
                                    $"Start Time: {process.StartTime}\n";
                    // Handle virtual memory size with error checking
                    try
                    {
                        details += $"Virtual Memory: {process.VirtualMemorySize64 / (1024 * 1024)} MB\n";
                    }
                    catch (Exception ex)
                    {
                        details += $"Virtual Memory: Error - {ex.Message} (Run as Administrator may be required)\n";
                    }

                    MessageBox.Show(details, "Process Details");
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("The process has terminated or is no longer available.", "Error");
                }
                catch (Win32Exception ex)
                {
                    MessageBox.Show($"Unable to access process details: {ex.Message} (Run as Administrator may be required)", "Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected error: {ex.Message}", "Error");
                }
            }
        }

        private void killProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxProcesses.SelectedIndex != -1)
            {
                string selectedItem = listBoxProcesses.SelectedItem.ToString();
                int processId = int.Parse(selectedItem.Split('-')[0].Trim());
                if (MessageBox.Show($"Are you sure you want to kill process {processId}?", "Confirm Kill", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        Process process = Process.GetProcessById(processId);
                        process.Kill();
                        LoadProcesses(); // Refresh the list after killing
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("The process has terminated or is no longer available.", "Error");
                    }
                    catch (Win32Exception ex)
                    {
                        MessageBox.Show($"Unable to kill process: {ex.Message} (Run as Administrator may be required)", "Error");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Unexpected error killing process: {ex.Message}", "Error");
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.Title = "Export Process List";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            foreach (var item in listBoxProcesses.Items)
                            {
                                sw.WriteLine(item.ToString());
                            }
                            MessageBox.Show("Process list exported successfully!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting file: {ex.Message}", "Error");
                    }
                }
            }
        }

        private void btnShowThreadsModules_Click(object sender, EventArgs e)
        {
            if (listBoxProcesses.SelectedIndex != -1)
            {
                string selectedItem = listBoxProcesses.SelectedItem.ToString();
                int processId = int.Parse(selectedItem.Split('-')[0].Trim());
                try
                {
                    Process process = Process.GetProcessById(processId);
                    ProcessDetailsForm detailsForm = new ProcessDetailsForm(process);
                    detailsForm.ShowDialog();
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("The process has terminated or is no longer available.", "Error");
                }
                catch (Win32Exception ex)
                {
                    MessageBox.Show($"Unable to access process: {ex.Message} (Run as Administrator may be required)", "Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unexpected error: {ex.Message}", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please select a process first.");
            }
        }
    }

    // Updated form to display thread and module details with enhanced error handling
    public partial class ProcessDetailsForm : Form
    {
        private Process process;
        private TextBox txtDetails;

        public ProcessDetailsForm(Process process)
        {
            this.process = process ?? throw new ArgumentNullException(nameof(process)); // Ensure process is not null
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Threads and Modules";
            this.Size = new System.Drawing.Size(400, 300);

            txtDetails = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Font = new System.Drawing.Font("Consolas", 10)
            };

            this.Controls.Add(txtDetails);
            LoadDetails();
        }

        private void LoadDetails()
        {
            txtDetails.Clear();
            txtDetails.AppendText($"Process: {process.ProcessName} (ID: {process.Id})\n\n");

            txtDetails.AppendText("Threads:\n");
            try
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    txtDetails.AppendText($"  ID: {thread.Id}, Priority: {thread.PriorityLevel}, Start Time: {thread.StartTime}\n");
                }
            }
            catch (Exception ex)
            {
                txtDetails.AppendText($"  Error accessing threads: {ex.Message}\n");
            }

            txtDetails.AppendText("\nModules:\n");
            try
            {
                foreach (ProcessModule module in process.Modules)
                {
                    txtDetails.AppendText($"  Name: {module.ModuleName}, File: {module.FileName}, Memory Size: {module.ModuleMemorySize} bytes\n");
                }
            }
            catch (Win32Exception ex)
            {
                txtDetails.AppendText($"  Error accessing modules: {ex.Message} (Run as Administrator may be required)\n");
            }
            catch (Exception ex)
            {
                txtDetails.AppendText($"  Unexpected error accessing modules: {ex.Message}\n");
            }
        }
    }
}