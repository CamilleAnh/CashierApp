using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CashierApp
{
    public partial class InventoryForm : Form
    {
        private readonly SQLHelper _sqlHelper;

        public InventoryForm()
        {
            InitializeComponent();
            _sqlHelper = new SQLHelper();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                string query = "SELECT ProductID, Barcode, ProductName FROM Products";
                DataTable dt = _sqlHelper.ExecuteQuery(query);

                if (dt != null)
                {
                    lbProductList.DisplayMember = "ProductName";
                    lbProductList.ValueMember = "ProductID";
                    lbProductList.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbProductList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbProductList.SelectedItem != null)
            {
                try
                {
                    DataRowView selectedRow = (DataRowView)lbProductList.SelectedItem;
                    string productId = selectedRow["ProductID"].ToString();
                    string query = "SELECT * FROM Products WHERE ProductID=@ProductID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@ProductID", productId)
                    };
                    DataTable dt = _sqlHelper.ExecuteQueryWithParams(query, parameters);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        txtProductName.Text = row["ProductName"].ToString();
                        txtPrice.Text = row["Price"].ToString();
                        txtQuantity.Text = row["Quantity"].ToString();
                        txtBarcode.Text = row["Barcode"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading product details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ProductForm productForm = new ProductForm();
                productForm.ShowDialog();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lbProductList.SelectedItem != null)
            {
                try
                {
                    DataRowView selectedRow = (DataRowView)lbProductList.SelectedItem;
                    string productId = selectedRow["ProductID"].ToString();
                    ProductForm productForm = new ProductForm(productId);
                    productForm.ShowDialog();
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBarcode.Text))
            {
                try
                {
                    string barcode = txtBarcode.Text;
                    string query = "SELECT * FROM Products WHERE Barcode=@Barcode";
                    SqlParameter[] parameters = {
                        new SqlParameter("@Barcode", barcode)
                    };
                    DataTable dt = _sqlHelper.ExecuteQueryWithParams(query, parameters);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        txtProductName.Text = row["ProductName"].ToString();
                        txtPrice.Text = row["Price"].ToString();
                        txtQuantity.Text = row["Quantity"].ToString();
                        lbProductList.SelectedValue = row["ProductID"];
                    }
                    else
                    {
                        // Clear the fields if no product is found
                        txtProductName.Clear();
                        txtPrice.Clear();
                        txtQuantity.Clear();
                        lbProductList.ClearSelected();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error searching product by barcode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbProductList.SelectedItem != null)
            {
                try
                {
                    DataRowView selectedRow = (DataRowView)lbProductList.SelectedItem;
                    string productId = selectedRow["ProductID"].ToString();
                    string query = "DELETE FROM Products WHERE ProductID=@ProductID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@ProductID", productId)
                    };
                    _sqlHelper.ExecuteNonQueryWithParams(query, parameters);
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }
        private void splitContainer_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            // Initialization logic if needed
        }
    }
}

