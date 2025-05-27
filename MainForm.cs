using ClientInvoiceGenerator;
using ClientInvoiceGenerator.Data;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using static ClientInvoiceGenerator.Data.DbHelper;


namespace ClientInvoiceGenerator
{
    public partial class MainForm : Form
    {

        //private string connectionString = "Data Source=clients.db;Version=3;";
        private bool isLoading = false;

        public MainForm()
        {
            InitializeComponent();
            CreateTableIfNotExists();
            ClearInputs(); // Ensure form opens with empty inputs
            LoadClients();
          
        }

        private void CreateTableIfNotExists()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = @"CREATE TABLE IF NOT EXISTS Clients (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Name TEXT NOT NULL,
                                    Email TEXT NOT NULL,
                                    Address TEXT NOT NULL,
                                    UNIQUE(Name, Email)
                                 );";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadClients()
        {
            isLoading = true;

            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Clients";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvClients.DataSource = dt;
                }
            }

            dgvClients.ClearSelection();
            isLoading = false;
        }

        private void ClearInputs()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM Clients WHERE Name = @Name AND Email = @Email";
                using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("A client with the same name and email already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string insertQuery = "INSERT INTO Clients (Name, Email, Address) VALUES (@Name, @Email, @Address)";
                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
            }

            ClearInputs();
            LoadClients();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            if (dgvClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a client to update.", "Select Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(dgvClients.SelectedRows[0].Cells["Id"].Value);

            using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
            {
                conn.Open();
                string query = "UPDATE Clients SET Name = @Name, Email = @Email, Address = @Address WHERE Id = @Id";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            ClearInputs();
            LoadClients();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a client to delete.", "Select Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int id = Convert.ToInt32(dgvClients.SelectedRows[0].Cells["Id"].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete this client?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SQLiteConnection conn = new SQLiteConnection(DbHelper.ConnectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Clients WHERE Id = @Id";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                ClearInputs();
                LoadClients();
            }
        }

        private void dgvClients_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading || dgvClients.SelectedRows.Count == 0)
                return;

            txtName.Text = dgvClients.SelectedRows[0].Cells["Name"].Value.ToString();
            txtEmail.Text = dgvClients.SelectedRows[0].Cells["Email"].Value.ToString();
            txtAddress.Text = dgvClients.SelectedRows[0].Cells["Address"].Value.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
            dgvClients.ClearSelection();
        }

        private void btnOpenInvoices_Click(object sender, EventArgs e)
        {
            if (dgvClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a client first.", "Select Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int clientId = Convert.ToInt32(dgvClients.SelectedRows[0].Cells["Id"].Value);
            string clientName = dgvClients.SelectedRows[0].Cells["Name"].Value.ToString();

            InvoiceForm invoiceForm = new InvoiceForm(clientId, clientName);
            this.Hide();
            invoiceForm.ShowDialog();
            this.Show();
        }
    }
}
