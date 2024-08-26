using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashierApp
{
    public partial class InvoiceDetailsForm : Form
    {
        private int invoiceID;

        public InvoiceDetailsForm(int invoiceID)
        {
            InitializeComponent();
            this.invoiceID = invoiceID;
        }

        private void InvoiceDetailsForm_Load(object sender, EventArgs e)
        {
            LoadInvoiceDetails();
        }

        private void LoadInvoiceDetails()
        {
            // Fetch invoice details from the database for the given InvoiceID
            string query = "SELECT * FROM InvoiceDetails WHERE InvoiceID = @InvoiceID";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@InvoiceID", invoiceID)
            };
            SQLHelper sqlHelper = new SQLHelper();
            DataTable detailsTable = sqlHelper.ExecuteQuery(query, parameters);
            invoiceDetailsGridView.DataSource = detailsTable;
        }

        private void addDetailButton_Click(object sender, EventArgs e)
        {
            // Logic to add a new invoice detail
            string query = "INSERT INTO InvoiceDetails (InvoiceID, ProductID, Quantity, Price) VALUES (@InvoiceID, @ProductID, @Quantity, @Price)";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@InvoiceID", invoiceID),
            new SqlParameter("@ProductID", int.Parse(productIDTextBox.Text)),
            new SqlParameter("@Quantity", int.Parse(quantityTextBox.Text)),
            new SqlParameter("@Price", decimal.Parse(priceTextBox.Text))
            };
            SQLHelper sqlHelper = new SQLHelper();
            sqlHelper.ExecuteNonQuery(query, parameters);
            LoadInvoiceDetails();
        }

        private void editDetailButton_Click(object sender, EventArgs e)
        {
            // Logic to edit the selected invoice detail
            // Similar to Add, but with an UPDATE SQL command
        }

        private void deleteDetailButton_Click(object sender, EventArgs e)
        {
            // Logic to delete the selected invoice detail
            // Use DELETE SQL command
        }
    }

}
