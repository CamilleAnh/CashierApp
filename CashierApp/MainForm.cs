using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CashierApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;  // Make the form maximized
         
            this.StartPosition = FormStartPosition.Manual; // Set start position
            this.Bounds = Screen.PrimaryScreen.Bounds;     // Set form to fill the entire screen
        }


        private void quảnLýKhoHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryForm inventoryForm = new InventoryForm();
            inventoryForm.Show();
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckoutForm checkoutForm = new CheckoutForm();
            checkoutForm.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProductButtons();
            this.WindowState = FormWindowState.Maximized;
        
            this.StartPosition = FormStartPosition.Manual;
            this.Bounds = Screen.PrimaryScreen.Bounds;
        }

        private void LoadProductButtons()
        {
            // Load sản phẩm từ cơ sở dữ liệu
            SQLHelper sqlHelper = new SQLHelper();
            string query = "SELECT ProductID, ProductName, Price FROM Products";
            DataTable productsTable = sqlHelper.ExecuteQuery(query);

            foreach (DataRow row in productsTable.Rows)
            {
                string productName = row["ProductName"].ToString();
                decimal price = (decimal)row["Price"];
                int productId = (int)row["ProductID"];

                Button productButton = new Button
                {
                    Text = productName,
                    Tag = new { ProductID = productId, Price = price }, // Store product ID and price in the button's Tag property
                    Width = 200,
                    Height = 50,
                    Margin = new Padding(10)
                };
                productButton.Click += (s, e) => AddProductToInvoice(productButton);
                productPanel.Controls.Add(productButton);
            }
        }

        private void AddProductToInvoice(Button productButton)
        {
            dynamic productInfo = productButton.Tag;
            string productName = productButton.Text;
            decimal price = productInfo.Price;

            int index = invoiceGrid.Rows.Add();
            invoiceGrid.Rows[index].Cells["ProductName"].Value = productName;
            invoiceGrid.Rows[index].Cells["UnitPrice"].Value = price;
            invoiceGrid.Rows[index].Cells["Quantity"].Value = 1;
            invoiceGrid.Rows[index].Cells["Total"].Value = price;

            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            double total = 0.00;
            foreach (DataGridViewRow row in invoiceGrid.Rows)
            {
                total += Convert.ToDouble(row.Cells["Total"].Value);
            }
            totalLabel.Text = $"Total: ${total:F2}";
        }

        private void SearchProducts(string query)
        {
            SQLHelper sqlHelper = new SQLHelper();
            string sqlQuery = "SELECT ProductID, ProductName, Price FROM Products WHERE ProductName LIKE @query OR Barcode LIKE @query";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@query", "%" + query + "%")
            };
            DataTable productsTable = sqlHelper.ExecuteQueryWithParams(sqlQuery, parameters);

            productPanel.Controls.Clear(); // Xóa các nút sản phẩm cũ

            foreach (DataRow row in productsTable.Rows)
            {
                string productName = row["ProductName"].ToString();
                decimal price = (decimal)row["Price"];
                int productId = (int)row["ProductID"];

                Button productButton = new Button
                {
                    Text = productName,
                    Tag = new { ProductID = productId, Price = price }, // Lưu trữ ProductID và giá vào thuộc tính Tag của nút
                    Width = 200,
                    Height = 50,
                    Margin = new Padding(10)
                };
                productButton.Click += (s, e) => AddProductToInvoice(productButton);
                productPanel.Controls.Add(productButton);
            }
        }


        private void payButton_Click(object sender, EventArgs e)
        {
            // Implement payment logic here
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchProducts(searchBox.Text);
        }
    }
}

