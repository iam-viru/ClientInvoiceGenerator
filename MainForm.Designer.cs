namespace ClientInvoiceGenerator
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvClients;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;

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
            dgvClients = new DataGridView();
            txtName = new TextBox();
            txtEmail = new TextBox();
            txtAddress = new TextBox();
            lblName = new Label();
            lblEmail = new Label();
            lblAddress = new Label();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnOpenInvoices = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClients).BeginInit();
            SuspendLayout();
            // 
            // dgvClients
            // 
            dgvClients.AllowUserToAddRows = false;
            dgvClients.AllowUserToDeleteRows = false;
            dgvClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClients.Location = new Point(20, 180);
            dgvClients.MultiSelect = false;
            dgvClients.Name = "dgvClients";
            dgvClients.ReadOnly = true;
            dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClients.Size = new Size(500, 200);
            dgvClients.TabIndex = 0;
            dgvClients.SelectionChanged += dgvClients_SelectionChanged;
            // 
            // txtName
            // 
            txtName.Location = new Point(100, 20);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 23);
            txtName.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(100, 60);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 23);
            txtEmail.TabIndex = 2;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(100, 100);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(300, 23);
            txtAddress.TabIndex = 3;
            // 
            // lblName
            // 
            lblName.Location = new Point(20, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(80, 23);
            lblName.TabIndex = 4;
            lblName.Text = "Name:";
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(20, 60);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(80, 23);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email:";
            // 
            // lblAddress
            // 
            lblAddress.Location = new Point(20, 100);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(80, 23);
            lblAddress.TabIndex = 6;
            lblAddress.Text = "Address:";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(420, 20);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 23);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Add Client";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(420, 60);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 23);
            btnUpdate.TabIndex = 8;
            btnUpdate.Text = "Update Client";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(420, 100);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 23);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Delete Client";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnOpenInvoices
            // 
            btnOpenInvoices.Location = new Point(526, 180);
            btnOpenInvoices.Name = "btnOpenInvoices";
            btnOpenInvoices.Size = new Size(99, 23);
            btnOpenInvoices.TabIndex = 10;
            btnOpenInvoices.Text = "Open Invoices";
            btnOpenInvoices.UseVisualStyleBackColor = true;
            btnOpenInvoices.Click += btnOpenInvoices_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(637, 400);
            Controls.Add(btnOpenInvoices);
            Controls.Add(dgvClients);
            Controls.Add(txtName);
            Controls.Add(txtEmail);
            Controls.Add(txtAddress);
            Controls.Add(lblName);
            Controls.Add(lblEmail);
            Controls.Add(lblAddress);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Name = "MainForm";
            Text = "Client Management";
            ((System.ComponentModel.ISupportInitialize)dgvClients).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Button btnOpenInvoices;
    }
}
