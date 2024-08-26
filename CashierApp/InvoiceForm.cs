using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CashierApp
{
    public partial class InvoiceForm : Form
    {
        public InvoiceForm()
        {
            InitializeComponent();
            invoicesGridView.ReadOnly = true; // Make the DataGridView read-only
            invoicesGridView.SelectionChanged += invoicesGridView_SelectionChanged;
        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            LoadInvoices();
            // Load cashiers into the ComboBox
        }

        private void LoadInvoices()
        {
            // Fetch invoices from the database and bind to DataGridView
            string query = "SELECT * FROM Invoices";
            SQLHelper sqlHelper = new SQLHelper();
            DataTable invoicesTable = sqlHelper.ExecuteQuery(query);
            invoicesGridView.DataSource = invoicesTable;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Logic to add a new invoice
            string query = "INSERT INTO Invoices (InvoiceNumber, TotalAmount, InvoiceDate, CashierID) VALUES (@InvoiceNumber, @TotalAmount, @InvoiceDate, @CashierID)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@InvoiceNumber", invoiceNumberTextBox.Text),
                new SqlParameter("@TotalAmount", decimal.Parse(totalAmountTextBox.Text)),
                new SqlParameter("@InvoiceDate", DateTime.Parse(invoiceDateTextBox.Text)),
                new SqlParameter("@CashierID", int.Parse(cashierComboBox.SelectedValue.ToString()))
            };
            SQLHelper sqlHelper = new SQLHelper();
            sqlHelper.ExecuteNonQuery(query, parameters);
            LoadInvoices();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            // Logic to edit the selected invoice
            if (invoicesGridView.SelectedRows.Count > 0)
            {
                int selectedInvoiceID = Convert.ToInt32(invoicesGridView.SelectedRows[0].Cells["InvoiceID"].Value);
                string query = "UPDATE Invoices SET InvoiceNumber = @InvoiceNumber, TotalAmount = @TotalAmount, InvoiceDate = @InvoiceDate, CashierID = @CashierID WHERE InvoiceID = @InvoiceID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceNumber", invoiceNumberTextBox.Text),
                    new SqlParameter("@TotalAmount", decimal.Parse(totalAmountTextBox.Text)),
                    new SqlParameter("@InvoiceDate", DateTime.Parse(invoiceDateTextBox.Text)),
                    new SqlParameter("@CashierID", int.Parse(cashierComboBox.SelectedValue.ToString())),
                    new SqlParameter("@InvoiceID", selectedInvoiceID)
                };
                SQLHelper sqlHelper = new SQLHelper();
                sqlHelper.ExecuteNonQuery(query, parameters);
                LoadInvoices();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Logic to delete the selected invoice
            if (invoicesGridView.SelectedRows.Count > 0)
            {
                int selectedInvoiceID = Convert.ToInt32(invoicesGridView.SelectedRows[0].Cells["InvoiceID"].Value);
                string query = "DELETE FROM Invoices WHERE InvoiceID = @InvoiceID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@InvoiceID", selectedInvoiceID)
                };
                SQLHelper sqlHelper = new SQLHelper();
                sqlHelper.ExecuteNonQuery(query, parameters);
                LoadInvoices();
            }
        }

        private void viewDetailsButton_Click(object sender, EventArgs e)
        {
            // Open InvoiceDetailsForm and pass the selected InvoiceID
            if (invoicesGridView.SelectedRows.Count > 0)
            {
                int selectedInvoiceID = Convert.ToInt32(invoicesGridView.SelectedRows[0].Cells["InvoiceID"].Value);
                InvoiceDetailsForm detailsForm = new InvoiceDetailsForm(selectedInvoiceID);
                detailsForm.Show();
            }
        }

        private void invoicesGridView_SelectionChanged(object sender, EventArgs e)
        {
            // Display selected invoice details in the input fields
            if (invoicesGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = invoicesGridView.SelectedRows[0];
                invoiceNumberTextBox.Text = selectedRow.Cells["InvoiceNumber"].Value.ToString();
                totalAmountTextBox.Text = selectedRow.Cells["TotalAmount"].Value.ToString();
                invoiceDateTextBox.Text = selectedRow.Cells["InvoiceDate"].Value.ToString();
                cashierComboBox.SelectedValue = selectedRow.Cells["CashierID"].Value;
            }
        }

        private void cashierComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cashierComboBox.SelectedValue != null)
            {
                int selectedUserId = (int)cashierComboBox.SelectedValue;

                // Query to fetch additional user details based on the selected user ID
                string query = "SELECT Username, Role FROM Users WHERE UserID = @UserID";
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@UserID", selectedUserId)
                };

                SQLHelper sqlHelper = new SQLHelper();
                DataTable userDetailsTable = sqlHelper.ExecuteQuery(query, parameters);

                if (userDetailsTable.Rows.Count > 0)
                {
                    DataRow row = userDetailsTable.Rows[0];
                    string username = row["Username"].ToString();
                    string role = row["Role"].ToString();

                    // Display or use these details as needed
                    MessageBox.Show($"Selected User: {username}\nRole: {role}", "User Details");

                    // Or you can populate other form fields with this data
                    // usernameTextBox.Text = username;
                    // roleTextBox.Text = role;
                }
            }
        }
}
