using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientInvoiceGenerator.Data
{
    public static class DatabaseHelper
    {
        private static string dbFile = "invoices.db";
        public static string ConnectionString => $"Data Source={dbFile};Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
                using (var connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string createClients = @"
                        CREATE TABLE Clients (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Email TEXT,
                            Phone TEXT,
                            Address TEXT
                        );";

                    string createInvoices = @"
                        CREATE TABLE Invoices (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            ClientId INTEGER,
                            InvoiceDate TEXT,
                            TotalAmount REAL,
                            FOREIGN KEY(ClientId) REFERENCES Clients(Id)
                        );";

                    string createInvoiceItems = @"
                        CREATE TABLE InvoiceItems (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            InvoiceId INTEGER,
                            Description TEXT,
                            Quantity INTEGER,
                            Rate REAL,
                            Amount REAL,
                            FOREIGN KEY(InvoiceId) REFERENCES Invoices(Id)
                        );";

                    var command = connection.CreateCommand();
                    command.CommandText = createClients + createInvoices + createInvoiceItems;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
