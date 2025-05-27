using ClientInvoiceGenerator.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using static ClientInvoiceGenerator.Data.DbHelper;

namespace ClientInvoiceGenerator
{
    public partial class InvoiceForm : Form
    {
        private int selectedInvoiceId = -1;
        private int clientId;
        private string clientName;

        public InvoiceForm(int clientId, string clientName)
        {
            InitializeComponent();
            this.clientId = clientId;
            this.clientName = clientName;

            // Use client info, e.g.:
            lblClientName.Text = clientName;
            LoadClients();
            LoadInvoices();
            //LoadInvoicesForClient();`
        }
        public InvoiceForm()
        {
            InitializeComponent();
            this.Load += InvoiceForm_Load;

        }
        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            LoadClientsIntoDropdown();
        }
        private void LoadClientsIntoDropdown()
        {
            cmbClients.Items.Clear();
            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = "SELECT Id, Name FROM Clients ORDER BY Name";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbClients.Items.Add(new ComboBoxItem
                            {
                                Text = reader["Name"].ToString(),
                                Value = Convert.ToInt32(reader["Id"])
                            });
                        }
                    }
                }
            }

            if (cmbClients.Items.Count > 0)
            {
                cmbClients.SelectedIndex = 0;
            }
        }


        private void LoadClients()
        {
            cmbClients.Items.Clear();

            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = "SELECT Id, Name FROM Clients";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbClients.Items.Add(new ComboBoxItem
                        {
                            Text = reader["Name"]?.ToString() ?? string.Empty, // null-coalescing operator to handle potential null values
                            Value = Convert.ToInt32(reader["Id"])
                        });
                    }
                }
            }

            if (cmbClients.Items.Count > 0)
                cmbClients.SelectedIndex = 0;
        }


        private void LoadInvoices()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT Invoices.Id, Clients.Name AS ClientName, Invoices.Date, Invoices.Amount, Invoices.Description
                    FROM Invoices
                    INNER JOIN Clients ON Invoices.ClientId = Clients.Id";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvInvoices.DataSource = table;
                }
            }
            dgvInvoices.Columns["Id"].Visible = false;
        }

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            // 1. Validate client selection
            if (!(cmbClients.SelectedItem is ComboBoxItem selectedClient))
            {
                MessageBox.Show("Please select a valid client.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int clientId = selectedClient.Value;

            // 2. Validate description
            string description = txtDescription.Text.Trim();
            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please enter a description for the invoice.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Validate amount
            string amountText = txtAmount.Text.Trim();
            if (!decimal.TryParse(amountText, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Validate invoice date (optional – mostly always valid)
            string invoiceDate = dtpDate.Value.ToString("yyyy-MM-dd");

            // All validations passed – Insert into DB
            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Invoices (ClientId, Description, Amount, Date)
                         VALUES (@ClientId, @Description, @Amount, @Date)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@Date", invoiceDate);
                    cmd.ExecuteNonQuery();
                }
            }

            // Feedback & Reset
            MessageBox.Show("Invoice added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtDescription.Clear();
            txtAmount.Clear();

            LoadClients();
        }



        private void btnUpdateInvoice_Click(object sender, EventArgs e)
        {
            if (selectedInvoiceId == -1 || cmbClients.SelectedItem == null) return;

            var client = (ComboBoxItem)cmbClients.SelectedItem;
            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = "UPDATE Invoices SET ClientId = @ClientId, Date = @Date, Amount = @Amount, Description = @Description WHERE Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientId", client.Value);
                    cmd.Parameters.AddWithValue("@Date", dtpDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Id", selectedInvoiceId);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadInvoices();
            ClearFields();
        }

        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            if (selectedInvoiceId == -1) return;

            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM Invoices WHERE Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", selectedInvoiceId);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadInvoices();
            ClearFields();
        }

        private void dgvInvoices_SelectionChanged(object sender, EventArgs e)
        {
            //if (dgvInvoices.SelectedRows.Count > 0)
            //{
            //    var row = dgvInvoices.SelectedRows[0];
            //    selectedInvoiceId = Convert.ToInt32(row.Cells["Id"].Value);
            //    cmbClients.Text = row.Cells["ClientName"].Value.ToString();
            //    dtpDate.Value = Convert.ToDateTime(row.Cells["Date"].Value);
            //    txtAmount.Text = row.Cells["Amount"].Value.ToString();
            //    txtDescription.Text = row.Cells["Description"].Value.ToString();
            //}
        }

        private void ClearFields()
        {
            selectedInvoiceId = -1;
            dtpDate.Value = DateTime.Now;
            txtAmount.Text = "";
            txtDescription.Text = "";
            if (cmbClients.Items.Count > 0)
                cmbClients.SelectedIndex = 0;
        }

        private void btnGeneratePdf_Click(object sender, EventArgs e)
        {
            if (!(cmbClients.SelectedItem is ComboBoxItem selectedClient))
            {
                MessageBox.Show("Select a valid client first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int clientId = selectedClient.Value;
            string clientName = selectedClient.Text;

            DataTable invoiceTable = new DataTable();

            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = "SELECT Description, Amount, Date FROM Invoices WHERE ClientId = @ClientId";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        adapter.Fill(invoiceTable);
                    }
                }
            }

            if (invoiceTable.Rows.Count == 0)
            {
                MessageBox.Show("No invoices found for this client.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Choose save location
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"Invoice_{clientName.Replace(" ", "_")}.pdf"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            // Create PDF
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
            doc.Open();

            doc.Add(new Paragraph($"Invoices for {clientName}", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)));
            doc.Add(new Paragraph($"Date: {DateTime.Now.ToShortDateString()}"));
            doc.Add(new Paragraph(" ")); // blank line

            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 50f, 25f, 25f });

            // Add headers
            table.AddCell("Description");
            table.AddCell("Amount");
            table.AddCell("Date");

            decimal total = 0;

            foreach (DataRow row in invoiceTable.Rows)
            {
                table.AddCell(row["Description"].ToString());
                table.AddCell(Convert.ToDecimal(row["Amount"]).ToString("C"));
                table.AddCell(Convert.ToDateTime(row["Date"]).ToShortDateString());

                total += Convert.ToDecimal(row["Amount"]);
            }

            // Add total row
            table.AddCell("TOTAL");
            table.AddCell(total.ToString("C"));
            table.AddCell("");

            doc.Add(table);
            doc.Close();

            MessageBox.Show("PDF generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
