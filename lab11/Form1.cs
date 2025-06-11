using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ContactsApp
{
    public partial class Form1 : Form
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter adapter;
        private DataSet dataSet;
        private string dbPath = "Contacts.db";
        private string imgFolder = "images\\";

        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadData();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }

            connection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            connection.Open();

            // Create ContactGroups table
            string createGroupsTable = @"
        CREATE TABLE IF NOT EXISTS ContactGroups (
            group_id INTEGER PRIMARY KEY AUTOINCREMENT,
            group_name TEXT NOT NULL
        )";
            new SQLiteCommand(createGroupsTable, connection).ExecuteNonQuery();

            // Create Contacts table
            string createContactsTable = @"
        CREATE TABLE IF NOT EXISTS Contacts (
            cid INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            phone TEXT,
            email TEXT,
            img TEXT,
            group_id INTEGER,
            FOREIGN KEY (group_id) REFERENCES ContactGroups(group_id)
        )";
            new SQLiteCommand(createContactsTable, connection).ExecuteNonQuery();

            // Update existing group names to new names
            string[] oldNames = { "Family", "Work", "Friends" }; // Old names to replace
            string[] newNames = { "Касса", "Водій", "Кондуктор" }; // New names
            for (int i = 0; i < oldNames.Length; i++)
            {
                string updateQuery = "UPDATE ContactGroups SET group_name = @newName WHERE group_name = @oldName";
                SQLiteCommand cmdUpdate = new SQLiteCommand(updateQuery, connection);
                cmdUpdate.Parameters.AddWithValue("@newName", newNames[i]);
                cmdUpdate.Parameters.AddWithValue("@oldName", oldNames[i]);
                cmdUpdate.ExecuteNonQuery();
            }

            // Insert default groups only if no groups exist
            string checkGroups = "SELECT COUNT(*) FROM ContactGroups";
            SQLiteCommand cmd = new SQLiteCommand(checkGroups, connection);
            if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
            {
                string insertGroups = @"
            INSERT INTO ContactGroups (group_name) VALUES ('Касса');
            INSERT INTO ContactGroups (group_name) VALUES ('Водій');
            INSERT INTO ContactGroups (group_name) VALUES ('Кондуктор')";
                new SQLiteCommand(insertGroups, connection).ExecuteNonQuery();
            }

            connection.Close();
        }

        private void LoadData()
        {
            connection.Open();

            // Load groups into ComboBox
            comboBoxGroup.Items.Clear();
            string selectGroups = "SELECT * FROM ContactGroups";
            SQLiteDataAdapter groupAdapter = new SQLiteDataAdapter(selectGroups, connection);
            DataTable groupTable = new DataTable();
            groupAdapter.Fill(groupTable);
            foreach (DataRow row in groupTable.Rows)
            {
                comboBoxGroup.Items.Add(new { Text = row["group_name"], Value = row["group_id"] });
            }
            comboBoxGroup.DisplayMember = "Text";
            comboBoxGroup.ValueMember = "Value";

            // Clear existing bindings to avoid duplicates
            textBoxName.DataBindings.Clear();
            textBoxPhone.DataBindings.Clear();
            textBoxEmail.DataBindings.Clear();
            textBoxImage.DataBindings.Clear();

            // Load contacts into DataGridView
            dataSet = new DataSet();
            string selectContacts = "SELECT c.cid, c.name, c.phone, c.email, c.img, g.group_name " +
                                   "FROM Contacts c LEFT JOIN ContactGroups g ON c.group_id = g.group_id";
            adapter = new SQLiteDataAdapter(selectContacts, connection);
            SQLiteCommandBuilder builder = new SQLiteCommandBuilder(adapter);
            adapter.Fill(dataSet, "Contacts");
            dataGridView1.DataSource = dataSet.Tables["Contacts"];
            dataGridView1.Columns["cid"].Visible = false;
            dataGridView1.Columns["img"].Visible = false;

            // Bind textboxes (only once)
            textBoxName.DataBindings.Add("Text", dataSet.Tables["Contacts"], "name");
            textBoxPhone.DataBindings.Add("Text", dataSet.Tables["Contacts"], "phone");
            textBoxEmail.DataBindings.Add("Text", dataSet.Tables["Contacts"], "email");
            textBoxImage.DataBindings.Add("Text", dataSet.Tables["Contacts"], "img");

            connection.Close();
            UpdatePictureBox();
        }

        private void buttonSortByGroup_Click(object sender, EventArgs e)
        {
            try
            {
                // Створюємо DataView на основі таблиці DataSet
                DataView dataView = new DataView(dataSet.Tables["Contacts"]);

                // Сортуємо за стовпцем group_name
                dataView.Sort = "group_name ASC";

                // Оновлюємо DataGridView з відсортованим видом
                dataGridView1.DataSource = dataView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка сортування за групою: {ex.Message}");
            }
        }
        private void UpdatePictureBox()
        {
            string imagePath = string.IsNullOrEmpty(textBoxImage.Text) ? $"{imgFolder}nobody.jpg" : $"{imgFolder}{textBoxImage.Text}";
            try
            {
                if (File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                    toolTip1.SetToolTip(pictureBox1, $"Зображення завантажено: {imagePath}");
                }
                else
                {
                    pictureBox1.Image = null;
                    toolTip1.SetToolTip(pictureBox1, $"Зображення не знайдено: {imagePath}");
                }
            }
            catch (Exception ex)
            {
                pictureBox1.Image = null;
                toolTip1.SetToolTip(pictureBox1, $"Помилка при завантаженні зображення: {ex.Message}");
            }
            pictureBox1.Refresh(); // Ensure the PictureBox updates
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Виберіть фото";
            openFileDialog1.InitialDirectory = Path.GetFullPath(imgFolder);
            openFileDialog1.Filter = "Images|*.jpg;*.png|All Files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sourcePath = openFileDialog1.FileName;
                string fileName = Path.GetFileName(sourcePath);
                string destPath = Path.Combine(imgFolder, fileName);

                // Create images folder if it doesn't exist
                if (!Directory.Exists(imgFolder))
                {
                    Directory.CreateDirectory(imgFolder);
                }

                // Copy the image to the images folder if it doesn't exist or overwrite if it does
                File.Copy(sourcePath, destPath, true); // true allows overwriting
                textBoxImage.Text = fileName;
                UpdatePictureBox();
            }
        }

        private void textBoxImage_TextChanged(object sender, EventArgs e)
        {
            UpdatePictureBox();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string insert = "INSERT INTO Contacts (name, phone, email, img, group_id) VALUES (@name, @phone, @email, @img, @group_id)";
                SQLiteCommand cmd = new SQLiteCommand(insert, connection);
                cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                cmd.Parameters.AddWithValue("@phone", textBoxPhone.Text);
                cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                cmd.Parameters.AddWithValue("@img", textBoxImage.Text);
                cmd.Parameters.AddWithValue("@group_id", comboBoxGroup.SelectedItem != null ? ((dynamic)comboBoxGroup.SelectedItem).Value : DBNull.Value);
                cmd.ExecuteNonQuery();
                connection.Close();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding contact: {ex.Message}");
                connection.Close();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                adapter.Update(dataSet, "Contacts");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка оновлення данних: {ex.Message}");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ви впевнені що хочете видалити цей запис?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    try
                    {
                        connection.Open();
                        int cid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["cid"].Value);
                        string deleteQuery = "DELETE FROM Contacts WHERE cid = @cid";
                        SQLiteCommand cmd = new SQLiteCommand(deleteQuery, connection);
                        cmd.Parameters.AddWithValue("@cid", cid);
                        cmd.ExecuteNonQuery();

                        // Remove the row from the DataGridView and refresh the DataSet
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                        dataSet.Tables["Contacts"].AcceptChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка видалення записуt: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText)) return;

            connection.Open();
            string select = "SELECT c.cid, c.name, c.phone, c.email, c.img, g.group_name " +
                            "FROM Contacts c LEFT JOIN ContactGroups g ON c.group_id = g.group_id " +
                            "WHERE c.name LIKE @name";
            adapter = new SQLiteDataAdapter(select, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@name", $"%{searchText}%");
            dataSet.Clear();
            adapter.Fill(dataSet, "Contacts");
            dataGridView1.DataSource = dataSet.Tables["Contacts"];
            connection.Close();
        }

        private void buttonReportAll_Click(object sender, EventArgs e)
        {
            // Report 1: All Contacts
            Form reportForm = new Form { Text = "Звіт за всіма групами", Size = new Size(600, 400) };
            DataGridView reportGrid = new DataGridView { Dock = DockStyle.Fill, DataSource = dataSet.Tables["Contacts"] };
            reportForm.Controls.Add(reportGrid);
            reportForm.ShowDialog();
        }

        private void buttonReportByGroup_Click(object sender, EventArgs e)
        {
            // Report 2: Contacts by Group
            if (comboBoxGroup.SelectedItem == null) return;

            connection.Open();
            string select = "SELECT c.cid, c.name, c.phone, c.email, c.img, g.group_name " +
                            "FROM Contacts c LEFT JOIN ContactGroups g ON c.group_id = g.group_id " +
                            "WHERE c.group_id = @group_id";
            SQLiteDataAdapter reportAdapter = new SQLiteDataAdapter(select, connection);
            reportAdapter.SelectCommand.Parameters.AddWithValue("@group_id", ((dynamic)comboBoxGroup.SelectedItem).Value);
            DataTable reportTable = new DataTable();
            reportAdapter.Fill(reportTable);
            connection.Close();

            Form reportForm = new Form { Text = $"Звіт за посадою: {comboBoxGroup.Text}", Size = new Size(600, 400) };
            DataGridView reportGrid = new DataGridView { Dock = DockStyle.Fill, DataSource = reportTable };
            reportForm.Controls.Add(reportGrid);
            reportForm.ShowDialog();
        }

        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}