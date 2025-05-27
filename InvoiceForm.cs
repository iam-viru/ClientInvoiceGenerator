using ClientInvoiceGenerator.Data;
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
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cmbClients.DisplayMember = "Name";
                    cmbClients.ValueMember = "Id";
                    cmbClients.DataSource = dt;
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
            if (cmbClients.SelectedItem == null) return;

            var client = (ComboBoxItem)cmbClients.SelectedItem;
            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Invoices (ClientId, Date, Amount, Description) VALUES (@ClientId, @Date, @Amount, @Description)";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClientId", client.Value);
                    cmd.Parameters.AddWithValue("@Date", dtpDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadInvoices();
            ClearFields();
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
            if (dgvInvoices.SelectedRows.Count > 0)
            {
                var row = dgvInvoices.SelectedRows[0];
                selectedInvoiceId = Convert.ToInt32(row.Cells["Id"].Value);
                cmbClients.Text = row.Cells["ClientName"].Value.ToString();
                dtpDate.Value = Convert.ToDateTime(row.Cells["Date"].Value);
                txtAmount.Text = row.Cells["Amount"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
            }
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

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public ComboBoxItem(string text, int value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString() => Text;
        }
    }
}
