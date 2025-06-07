using System;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Lab12_COM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadDocumentTypes();
        }

        private void LoadDocumentTypes()
        {
            comboBoxTemplate.Items.Add("Візитки");
            comboBoxTemplate.Items.Add("Сертифікати");
            comboBoxTemplate.Items.Add("Рахунки");
            comboBoxTemplate.SelectedIndex = 0;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            Word.Application word = null;
            Word.Document doc = null;

            try
            {
                word = new Word.Application();
                doc = word.Documents.Add();

                // Create a table for 10 business cards (2 rows, 5 columns)
                Word.Table table = doc.Tables.Add(doc.Paragraphs[1].Range, 2, 5, ref missing, ref missing);
                table.Rows.Height = 100; // Adjust height as needed
                table.Columns.Width = 200; // Adjust width as needed

                // Populate the table with business card data
                int cardCount = 0;
                for (int i = 1; i <= table.Rows.Count; i++)
                {
                    for (int j = 1; j <= table.Columns.Count; j++)
                    {
                        if (cardCount < 10)
                        {
                            Word.Range cellRange = table.Cell(i, j).Range;
                            cellRange.Text = $"Company: {txtCompany.Text}\n" +
                                             $"Name: {txtName.Text}\n" +
                                             $"Surname: {txtSname.Text}\n" +
                                             $"Phone: {txtTel.Text}\n" +
                                             $"Email: {txtMail.Text}";
                            cardCount++;
                        }
                    }
                }

                // Show the document and prompt to save
                word.Visible = true;
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Word Documents (*.docx)|*.docx|All Files (*.*)|*.*",
                    Title = "Save Generated Document",
                    FileName = "BusinessCards.docx"
                };
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    doc.SaveAs2(saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nStack Trace: " + ex.StackTrace);
                if (doc != null) doc.Close();
                if (word != null) word.Quit();
            }
            finally
            {
                this.FormClosed += (s, args) =>
                {
                    if (doc != null) doc.Close();
                    if (word != null) word.Quit();
                    doc = null;
                    word = null;
                };
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Cleanup handled in buttonGenerate finally block
        }

        private object missing = System.Reflection.Missing.Value;

        private void comboBoxTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}