using System.Collections.Generic;
using System.Data.SQLite;

namespace ClientInvoiceGenerator.Data
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public static class ClientRepository
    {
        public static List<Client> GetAllClients()
        {
            var list = new List<Client>();
            using (var connection = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Clients", connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Client
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        Phone = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        Address = reader.IsDBNull(4) ? "" : reader.GetString(4)
                    });
                }
            }
            return list;
        }

        public static void AddClient(Client client)
        {
            using (var connection = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                var cmd = new SQLiteCommand("INSERT INTO Clients (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address)", connection);
                cmd.Parameters.AddWithValue("@Name", client.Name);
                cmd.Parameters.AddWithValue("@Email", client.Email);
                cmd.Parameters.AddWithValue("@Phone", client.Phone);
                cmd.Parameters.AddWithValue("@Address", client.Address);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateClient(Client client)
        {
            using (var connection = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                var cmd = new SQLiteCommand("UPDATE Clients SET Name=@Name, Email=@Email, Phone=@Phone, Address=@Address WHERE Id=@Id", connection);
                cmd.Parameters.AddWithValue("@Id", client.Id);
                cmd.Parameters.AddWithValue("@Name", client.Name);
                cmd.Parameters.AddWithValue("@Email", client.Email);
                cmd.Parameters.AddWithValue("@Phone", client.Phone);
                cmd.Parameters.AddWithValue("@Address", client.Address);
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteClient(int id)
        {
            using (var connection = new SQLiteConnection(DatabaseHelper.ConnectionString))
            {
                connection.Open();
                var cmd = new SQLiteCommand("DELETE FROM Clients WHERE Id=@Id", connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
