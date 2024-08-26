
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CashierApp
{
    public partial class MainForm : Form
    {
        private readonly SQLHelper _sqlHelper;

        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.Manual;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            invoiceGrid.CellClick += invoiceGrid_CellClick;

            _sqlHelper = new SQLHelper(); // Initialize SQLHelper
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeInvoiceGrid(); // Initialize the DataGridView columns
            LoadProductButtons();     // Load product buttons on form load
        }


        private void invoiceGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo rằng hàng được chọn hợp lệ
            {
                var row = invoiceGrid.Rows[e.RowIndex];

                if (e.ColumnIndex == invoiceGrid.Columns["IncreaseButton"].Index)
                {
                    // Tăng số lượng
                    int currentQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                    row.Cells["Quantity"].Value = currentQuantity + 1;
                    row.Cells["Total"].Value = (currentQuantity + 1) * unitPrice;
                    UpdateTotalAmount();
                }
                else if (e.ColumnIndex == invoiceGrid.Columns["DecreaseButton"].Index)
                {
                    // Giảm số lượng
                    int currentQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    if (currentQuantity > 1)
                    {
                        decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                        row.Cells["Quantity"].Value = currentQuantity - 1;
                        row.Cells["Total"].Value = (currentQuantity - 1) * unitPrice;
                        UpdateTotalAmount();
                    }
                }
                else if (e.ColumnIndex == invoiceGrid.Columns["DeleteButton"].Index)
                {
                    // Xóa hàng
                    var result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        invoiceGrid.Rows.RemoveAt(e.RowIndex);
                        UpdateTotalAmount();
                    }
                }
            }
        }

        private void invoiceGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = invoiceGrid.Rows[e.RowIndex];

                if (e.ColumnIndex == invoiceGrid.Columns["IncreaseQuantity"].Index)
                {
                    // Tăng số lượng
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                    row.Cells["Quantity"].Value = quantity + 1;
                    row.Cells["Total"].Value = (quantity + 1) * unitPrice;
                    UpdateTotalAmount();
                }
                else if (e.ColumnIndex == invoiceGrid.Columns["DecreaseQuantity"].Index)
                {
                    // Giảm số lượng
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    if (quantity > 1)
                    {
                        decimal unitPrice = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                        row.Cells["Quantity"].Value = quantity - 1;
                        row.Cells["Total"].Value = (quantity - 1) * unitPrice;
                        UpdateTotalAmount();
                    }
                }
                else if (e.ColumnIndex == invoiceGrid.Columns["DeleteProduct"].Index)
                {
                    // Xóa sản phẩm
                    invoiceGrid.Rows.RemoveAt(e.RowIndex);
                    UpdateTotalAmount();
                }
            }
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

        private void LoadProductButtons()
        {
            string query = "SELECT ProductID, ProductName, Price FROM Products";
            DataTable productsTable = _sqlHelper.ExecuteQuery(query);

            productPanel.Controls.Clear(); // Clear existing product buttons

            foreach (DataRow row in productsTable.Rows)
            {
                string productName = row["ProductName"].ToString();
                decimal price = (decimal)row["Price"];
                int productId = (int)row["ProductID"];

                Button productButton = new Button
                {
                    Text = productName,
                    Tag = new { ProductID = productId, Price = price }, // Store ProductID and price in the Tag property
                    Width = 200,
                    Height = 50,
                    Margin = new Padding(10)
                };
                productButton.Click += ProductButton_Click;
                productPanel.Controls.Add(productButton);
            }
        }

        private void ProductButton_Click(object sender, EventArgs e)
        {
            Button productButton = sender as Button;
            if (productButton != null)
            {
                AddProductToInvoice(productButton);
            }
        }

        private void AddProductToInvoice(Button productButton)
        {
            dynamic productInfo = productButton.Tag;
            string productName = productButton.Text;
            decimal price = productInfo.Price;

            foreach (DataGridViewRow row in invoiceGrid.Rows)
            {
                if (row.Cells["ProductName"].Value != null && row.Cells["ProductName"].Value.ToString() == productName)
                {
                    int currentQuantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    row.Cells["Quantity"].Value = currentQuantity + 1;
                    row.Cells["Total"].Value = (currentQuantity + 1) * price;
                    UpdateTotalAmount();
                    return;
                }
            }

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
                if (row.Cells["Total"].Value != null)
                {
                    total += Convert.ToDouble(row.Cells["Total"].Value);
                }
            }
            totalLabel.Text = $"Tổng Tiền: {total:F2} VND";
        }

        private void SearchProducts(string query)
        {
            string sqlQuery = "SELECT ProductID, ProductName, Price FROM Products WHERE ProductName LIKE @query OR Barcode LIKE @query";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@query", "%" + query + "%")
            };
            DataTable productsTable = _sqlHelper.ExecuteQuery(sqlQuery, parameters);

            productPanel.Controls.Clear(); // Clear existing product buttons

            foreach (DataRow row in productsTable.Rows)
            {
                string productName = row["ProductName"].ToString();
                decimal price = (decimal)row["Price"];
                int productId = (int)row["ProductID"];

                Button productButton = new Button
                {
                    Text = productName,
                    Tag = new { ProductID = productId, Price = price }, // Store ProductID and price in the Tag property
                    Width = 200,
                    Height = 50,
                    Margin = new Padding(10)
                };
                productButton.Click += ProductButton_Click;
                productPanel.Controls.Add(productButton);
            }
        }

        private void payButton_Click(object sender, EventArgs e)
        {
            if (invoiceGrid.Rows.Count == 0)
            {
                MessageBox.Show("No items in the invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string invoiceNumber = GenerateInvoiceNumber();

            string createInvoiceQuery = "INSERT INTO Invoices (InvoiceNumber, TotalAmount, InvoiceDate) OUTPUT INSERTED.InvoiceID VALUES (@InvoiceNumber, @TotalAmount, @InvoiceDate)";
            SqlParameter[] invoiceParams = new SqlParameter[]
            {
                new SqlParameter("@InvoiceNumber", invoiceNumber),
                new SqlParameter("@TotalAmount", GetTotalAmount()),
                new SqlParameter("@InvoiceDate", DateTime.Now)
            };

            int invoiceId = (int)_sqlHelper.ExecuteScalarWithParams(createInvoiceQuery, invoiceParams);

            foreach (DataGridViewRow row in invoiceGrid.Rows)
            {
                if (row.Cells["ProductName"].Value != null)
                {
                    string productName = row.Cells["ProductName"].Value.ToString();
                    decimal price = Convert.ToDecimal(row.Cells["UnitPrice"].Value);
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                    string insertDetailQuery = "INSERT INTO InvoiceDetails (InvoiceID, ProductID, Quantity, Price) VALUES (@InvoiceID, @ProductID, @Quantity, @Price)";
                    SqlParameter[] detailParams = new SqlParameter[]
                    {
                        new SqlParameter("@InvoiceID", invoiceId),
                        new SqlParameter("@ProductID", GetProductID(productName)),
                        new SqlParameter("@Quantity", quantity),
                        new SqlParameter("@Price", price)
                    };
                    _sqlHelper.ExecuteNonQueryWithParams(insertDetailQuery, detailParams);

                    string updateProductQuantityQuery = "UPDATE Products SET Quantity = Quantity - @Quantity WHERE ProductID = @ProductID";
                    SqlParameter[] updateQuantityParams = new SqlParameter[]
                    {
                        new SqlParameter("@Quantity", quantity),
                        new SqlParameter("@ProductID", GetProductID(productName))
                    };
                    _sqlHelper.ExecuteNonQueryWithParams(updateProductQuantityQuery, updateQuantityParams);
                }
            }

            MessageBox.Show("Payment completed successfully!\nInvoice Number: " + invoiceNumber, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            invoiceGrid.Rows.Clear();
            UpdateTotalAmount();
        }

        private string GenerateInvoiceNumber()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private decimal GetTotalAmount()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in invoiceGrid.Rows)
            {
                if (row.Cells["Total"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }
            return total;
        }

        private int GetProductID(string productName)
        {
            string query = "SELECT ProductID FROM Products WHERE ProductName = @ProductName";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ProductName", productName)
            };
            object result = _sqlHelper.ExecuteScalarWithParams(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }
        private void InitializeInvoiceGrid()
        {
            invoiceGrid.Columns.Clear();

            invoiceGrid.Columns.Add("ProductName", "Product Name");
            invoiceGrid.Columns.Add("UnitPrice", "Unit Price");
            invoiceGrid.Columns.Add("Quantity", "Quantity");
            invoiceGrid.Columns.Add("Total", "Total");

            // Thêm các cột cho Tăng/Giảm/Xóa
            DataGridViewButtonColumn increaseColumn = new DataGridViewButtonColumn();
            increaseColumn.Name = "IncreaseQuantity";
            increaseColumn.HeaderText = "";
            increaseColumn.Text = "+";
            increaseColumn.UseColumnTextForButtonValue = true;
            invoiceGrid.Columns.Add(increaseColumn);

            DataGridViewButtonColumn decreaseColumn = new DataGridViewButtonColumn();
            decreaseColumn.Name = "DecreaseQuantity";
            decreaseColumn.HeaderText = "";
            decreaseColumn.Text = "-";
            decreaseColumn.UseColumnTextForButtonValue = true;
            invoiceGrid.Columns.Add(decreaseColumn);

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "DeleteProduct";
            deleteColumn.HeaderText = "";
            deleteColumn.Text = "X";
            deleteColumn.UseColumnTextForButtonValue = true;
            invoiceGrid.Columns.Add(deleteColumn);

            // Định dạng cột
            invoiceGrid.Columns["UnitPrice"].DefaultCellStyle.Format = "C2"; // Format as currency
            invoiceGrid.Columns["Quantity"].Width = 100;
            invoiceGrid.Columns["Total"].DefaultCellStyle.Format = "C2"; // Format as currency
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchProducts(searchBox.Text);
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm invoiceForm = new InvoiceForm();
            invoiceForm.Show();
        }

        private void totalLabel_Click(object sender, EventArgs e)
        {
            // You can implement some logic here if needed
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserManagementForm userManagementForm = new UserManagementForm();
            userManagementForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadProductButtons(); // Refresh product list when button is clicked
        }


    }
}