namespace ClientInvoiceGenerator
{
    partial class InvoiceForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.ComboBox cmbClients;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnAddInvoice;
        private System.Windows.Forms.Button btnUpdateInvoice;
        private System.Windows.Forms.Button btnDeleteInvoice;
        private System.Windows.Forms.DataGridView dgvInvoices;
        private System.Windows.Forms.Label lblClientName;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblClient = new Label();
            cmbClients = new ComboBox();
            lblDate = new Label();
            dtpDate = new DateTimePicker();
            lblAmount = new Label();
            txtAmount = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            btnAddInvoice = new Button();
            btnUpdateInvoice = new Button();
            btnDeleteInvoice = new Button();
            dgvInvoices = new DataGridView();
            lblClientName = new Label();
            btnGeneratePdf = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvInvoices).BeginInit();
            SuspendLayout();
            // 
            // lblClient
            // 
            lblClient.AutoSize = true;
            lblClient.Location = new Point(626, 27);
            lblClient.Name = "lblClient";
            lblClient.Size = new Size(41, 15);
            lblClient.TabIndex = 0;
            lblClient.Text = "Client:";
            // 
            // cmbClients
            // 
            cmbClients.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClients.Location = new Point(195, 20);
            cmbClients.Name = "cmbClients";
            cmbClients.Size = new Size(250, 23);
            cmbClients.TabIndex = 1;
            // 
            // lblDate
            // 
            lblDate.AutoSize = true;
            lblDate.Location = new Point(76, 60);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(34, 15);
            lblDate.TabIndex = 2;
            lblDate.Text = "Date:";
            // 
            // dtpDate
            // 
            dtpDate.Location = new Point(195, 60);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(250, 23);
            dtpDate.TabIndex = 3;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Location = new Point(76, 100);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(54, 15);
            lblAmount.TabIndex = 4;
            lblAmount.Text = "Amount:";
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(195, 100);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(250, 23);
            txtAmount.TabIndex = 5;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(76, 140);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(70, 15);
            lblDescription.TabIndex = 6;
            lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(195, 140);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(250, 60);
            txtDescription.TabIndex = 7;
            // 
            // btnAddInvoice
            // 
            btnAddInvoice.Location = new Point(506, 20);
            btnAddInvoice.Name = "btnAddInvoice";
            btnAddInvoice.Size = new Size(100, 30);
            btnAddInvoice.TabIndex = 8;
            btnAddInvoice.Text = "Add Invoice";
            btnAddInvoice.Click += btnAddInvoice_Click;
            // 
            // btnUpdateInvoice
            // 
            btnUpdateInvoice.Location = new Point(506, 60);
            btnUpdateInvoice.Name = "btnUpdateInvoice";
            btnUpdateInvoice.Size = new Size(100, 30);
            btnUpdateInvoice.TabIndex = 9;
            btnUpdateInvoice.Text = "Update Invoice";
            btnUpdateInvoice.Click += btnUpdateInvoice_Click;
            // 
            // btnDeleteInvoice
            // 
            btnDeleteInvoice.Location = new Point(506, 100);
            btnDeleteInvoice.Name = "btnDeleteInvoice";
            btnDeleteInvoice.Size = new Size(100, 30);
            btnDeleteInvoice.TabIndex = 10;
            btnDeleteInvoice.Text = "Delete Invoice";
            btnDeleteInvoice.Click += btnDeleteInvoice_Click;
            // 
            // dgvInvoices
            // 
            dgvInvoices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInvoices.Location = new Point(20, 206);
            dgvInvoices.Name = "dgvInvoices";
            dgvInvoices.Size = new Size(600, 200);
            dgvInvoices.TabIndex = 11;
            dgvInvoices.SelectionChanged += dgvInvoices_SelectionChanged;
            // 
            // lblClientName
            // 
            lblClientName.AutoSize = true;
            lblClientName.Font = new Font("Segoe UI", 12F);
            lblClientName.Location = new Point(20, 18);
            lblClientName.Name = "lblClientName";
            lblClientName.Size = new Size(103, 21);
            lblClientName.TabIndex = 0;
            lblClientName.Text = "Client Name: ";
            // 
            // btnGeneratePdf
            // 
            btnGeneratePdf.Location = new Point(506, 159);
            btnGeneratePdf.Name = "btnGeneratePdf";
            btnGeneratePdf.Size = new Size(100, 26);
            btnGeneratePdf.TabIndex = 12;
            btnGeneratePdf.Text = "Generate PDF";
            btnGeneratePdf.UseVisualStyleBackColor = true;
            btnGeneratePdf.Click += btnGeneratePdf_Click;
            // 
            // InvoiceForm
            // 
            ClientSize = new Size(721, 437);
            Controls.Add(btnGeneratePdf);
            Controls.Add(lblClient);
            Controls.Add(cmbClients);
            Controls.Add(lblDate);
            Controls.Add(dtpDate);
            Controls.Add(lblAmount);
            Controls.Add(txtAmount);
            Controls.Add(lblDescription);
            Controls.Add(txtDescription);
            Controls.Add(btnAddInvoice);
            Controls.Add(btnUpdateInvoice);
            Controls.Add(btnDeleteInvoice);
            Controls.Add(dgvInvoices);
            Controls.Add(lblClientName);
            Name = "InvoiceForm";
            Text = "Invoices";
            ((System.ComponentModel.ISupportInitialize)dgvInvoices).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Button btnGeneratePdf;
    }
}
