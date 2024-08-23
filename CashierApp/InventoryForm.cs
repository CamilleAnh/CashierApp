using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CashierApp
{
    public partial class InventoryForm : Form
    {
        private SQLHelper sqlHelper;

        public InventoryForm()
        {
            InitializeComponent();
            sqlHelper = new SQLHelper(); // Initialize SQLHelper instance
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                string query = "SELECT ProductID, Barcode, ProductName, Unit FROM Products";
                DataTable dt = sqlHelper.ExecuteQuery(query);

                if (dt != null)
                {
                    lbProductList.DisplayMember = "ProductName";
                    lbProductList.ValueMember = "ProductID";
                    lbProductList.DataSource = dt;

                    // Add "Add New Product" item to the end of the list
                    DataRow newRow = dt.NewRow();
                    newRow["ProductID"] = -1;  // Special ID for "Add New Product"
                    newRow["ProductName"] = "Add New Product";
                    dt.Rows.Add(newRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get current timestamp
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Prepare query and parameters
            string query = "";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Barcode", txtBarcode.Text),
        new SqlParameter("@ProductName", txtProductName.Text),
        new SqlParameter("@Price", Convert.ToDecimal(txtPrice.Text)),
        new SqlParameter("@Quantity", Convert.ToInt32(txtQuantity.Text)),
        new SqlParameter("@Unit", cmbUnit.SelectedItem?.ToString() ?? string.Empty),  // Use null-conditional operator
        new SqlParameter("@UpdatedAt", currentTime)
            };

            // Kiểm tra nếu `SelectedValue` là -1 hoặc `SelectedValue` là null thì thêm sản phẩm mới
            if (lbProductList.SelectedValue == null || (int)lbProductList.SelectedValue == -1)
            {
                // Insert case
                query = "INSERT INTO Products (Barcode, ProductName, Price, Quantity, Unit, CreatedAt, UpdatedAt) VALUES (@Barcode, @ProductName, @Price, @Quantity, @Unit, @UpdatedAt, @UpdatedAt)";
            }
            else // Nếu không thì cập nhật sản phẩm hiện tại
            {
                // Update case
                query = "UPDATE Products SET Barcode = @Barcode, ProductName = @ProductName, Price = @Price, Quantity = @Quantity, Unit = @Unit, UpdatedAt = @UpdatedAt WHERE ProductID = @ProductID";
                Array.Resize(ref parameters, parameters.Length + 1);
                parameters[parameters.Length - 1] = new SqlParameter("@ProductID", lbProductList.SelectedValue);
            }

            try
            {
                sqlHelper.ExecuteNonQueryWithParams(query, parameters);
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbProductList.SelectedValue != null)
            {
                string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ProductID", lbProductList.SelectedValue)
                };
                sqlHelper.ExecuteNonQueryWithParams(query, parameters);
                LoadProducts();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void lbProductList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lbProductList.SelectedItem != null)
                {
                    DataRowView selectedRow = (DataRowView)lbProductList.SelectedItem;
                    int productId = Convert.ToInt32(selectedRow["ProductID"]);

                    if (productId == -1) // "Add New Product"
                    {
                        // Clear all input fields for new product
                        txtProductName.Clear();
                        txtPrice.Clear();
                        txtQuantity.Clear();
                        txtBarcode.Clear();
                        cmbUnit.SelectedIndex = -1; // Clear the ComboBox selection
                    }
                    else
                    {
                        // Load the selected product's details
                        string query = "SELECT * FROM Products WHERE ProductID=@ProductID";
                        SqlParameter[] parameters = {
                    new SqlParameter("@ProductID", productId)
                };
                        DataTable dt = sqlHelper.ExecuteQueryWithParams(query, parameters);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];
                            txtProductName.Text = row["ProductName"].ToString();
                            txtPrice.Text = row["Price"].ToString();
                            txtQuantity.Text = row["Quantity"].ToString();
                            txtBarcode.Text = row["Barcode"].ToString();
                            cmbUnit.SelectedItem = row["Unit"].ToString(); // Set the selected unit
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }
