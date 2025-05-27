# 🧾 Client Invoice Generator (WinForms + SQLite)

A simple Windows Forms application built using **C# .NET** and **SQLite** that allows you to manage clients and generate invoices. Designed for freelancers, small businesses, or as a technical portfolio project.

---

## ✨ Features

- ✅ Add, update, and delete client records
- ✅ Prevent duplicate clients (by name and email)
- ✅ Generate invoices for individual clients
- ✅ View and manage client-specific invoices
- ✅ Input validation for all forms
- ✅ Clean UI with separate forms for Clients and Invoices
- ✅ Uses `OpenXML` (planned extension) for future PDF/Word invoice export
- ✅ Connection string centralized for easy environment updates

---
 

## 🛠️ Tech Stack

- **Frontend**: Windows Forms (.NET Framework or .NET Core)
- **Backend**: C#
- **Database**: SQLite
- **Architecture**: Modular Forms, ADO.NET, Static Config Class

---

## 📁 Project Structure

```plaintext
ClientInvoiceApp/
│
├── MainForm.cs             # Client management form
├── InvoiceForm.cs          # Invoice creation/viewing form
├── DbHelper.cs             # Static class for shared connection string
├── clients.db              # SQLite database (auto-generated)
├── README.md
└── *.Designer.cs           # Auto-generated UI layout files
