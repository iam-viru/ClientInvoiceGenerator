using ClientInvoiceGenerator.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ClientInvoiceGenerator
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            var clients = ClientRepository.GetAllClients();
            dgvClients.DataSource = clients;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var client = new Client
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text
            };
            ClientRepository.AddClient(client);
            LoadClients();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvClients.CurrentRow != null)
            {
                var client = (Client)dgvClients.CurrentRow.DataBoundItem;
                client.Name = txtName.Text;
                client.Email = txtEmail.Text;
                client.Phone = txtPhone.Text;
                client.Address = txtAddress.Text;

                ClientRepository.UpdateClient(client);
                LoadClients();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClients.CurrentRow != null)
            {
                var client = (Client)dgvClients.CurrentRow.DataBoundItem;
                ClientRepository.DeleteClient(client.Id);
                LoadClients();
            }
        }

        private void dgvClients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClients.CurrentRow != null)
            {
                var client = (Client)dgvClients.CurrentRow.DataBoundItem;
                txtName.Text = client.Name;
                txtEmail.Text = client.Email;
                txtPhone.Text = client.Phone;
                txtAddress.Text = client.Address;
            }
        }
    }
}
