using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Management;

namespace HardwareInfoApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); // Use the designer-generated InitializeComponent
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadHardwareInfo();
        }

        private void LoadHardwareInfo()
        {
            txtOutput.Clear();
            AppendHardwareInfo("Motherboard", "Win32_BaseBoard", new[] { "Manufacturer", "Product", "SerialNumber" });
            AppendHardwareInfo("Processor", "Win32_Processor", new[] { "Name", "Manufacturer", "Description" });
            AppendHardwareInfo("Graphics card", "Win32_VideoController", new[] { "Name", "VideoProcessor", "DriverVersion", "AdapterRAM" });
            AppendHardwareInfo("BIOS", "Win32_BIOS", new[] { "Manufacturer", "Version", "ReleaseDate" });
            AppendHardwareInfo("Physical Memory (RAM)", "Win32_PhysicalMemory", new[] { "Capacity", "Manufacturer", "Speed", "MemoryType" });
            AppendHardwareInfo("Disk Drive", "Win32_DiskDrive", new[] { "Caption", "Size" });
            AppendHardwareInfo("Operating System", "Win32_OperatingSystem", new[] { "Caption", "Version", "BuildNumber" });
            AppendHardwareInfo("Network Adapter", "Win32_NetworkAdapter", new[] { "Name", "MACAddress", "NetConnectionID" });
            AppendHardwareInfo("Network Configuration", "Win32_NetworkAdapterConfiguration", new[] { "Caption", "IPAddress", "IPEnabled" });
            AppendHardwareInfo("CD/DVD Drive", "Win32_CDROMDrive", new[] { "Name", "Drive" });

        }

        private void AppendHardwareInfo(string category, string wmiClass, string[] properties)
        {
            txtOutput.AppendText($"=== {category} ===\r\n");
            foreach (var property in properties)
            {
                List<string> results = GetHardwareInfo(wmiClass, property);
                if (results.Count > 0)
                {
                    txtOutput.AppendText($"{property}:\r\n");
                    foreach (var result in results)
                    {
                        // Convert Capacity from bytes to GB for readability
                        if (property == "Capacity" && long.TryParse(result, out long capacity))
                        {
                            double gb = capacity / (1024.0 * 1024 * 1024);
                            txtOutput.AppendText($"  {gb:F2} GB\r\n");
                        }
                        else
                        {
                            txtOutput.AppendText($"  {result}\r\n");
                        }
                    }
                }
            }
            txtOutput.AppendText("\r\n");
        }

        private List<string> GetHardwareInfo(string wmiClass, string classItemField)
        {
            List<string> result = new List<string>();
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM {wmiClass}");
                foreach (ManagementObject obj in searcher.Get())
                {
                    object value = obj[classItemField];
                    if (value != null)
                    {
                        if (value is Array array)
                        {
                            foreach (var item in array)
                            {
                                if (item != null)
                                    result.Add(item.ToString().Trim());
                            }
                        }
                        else
                        {
                            result.Add(value.ToString().Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                txtOutput.AppendText($"Error retrieving {classItemField} from {wmiClass}: {ex.Message}\r\n");
            }
            return result;
        }
    }
}