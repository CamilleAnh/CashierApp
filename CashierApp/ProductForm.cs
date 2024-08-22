using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CashierApp
{
    public partial class ProductForm : Form
    {
        private readonly SQLHelper _sqlHelper;
        private string productId;

        public ProductForm(string productId = null)
        {
            InitializeComponent();
            _sqlHelper = new SQLHelper();
            this.productId = productId;
            if (!string.IsNullOrEmpty(productId))
            {
                LoadProductData(productId);
            }
        }

        private void LoadProductData(string productId)
        {
            string query = "SELECT * FROM Products WHERE ProductID=@ProductID";
            SqlParameter[] parameters = {
                new SqlParameter("@ProductID", productId)
            };
            var dt = _sqlHelper.ExecuteQueryWithParams(query, parameters);

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                txtBarcode.Text = row["Barcode"].ToString();
                txtProductName.Text = row["ProductName"].ToString();
                txtPrice.Text = row["Price"].ToString();
                txtQuantity.Text = row["Quantity"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query;
            SqlParameter[] parameters;

            if (string.IsNullOrEmpty(productId))
            {
                query = "INSERT INTO Products (Barcode, ProductName, Price, Quantity) VALUES (@Barcode, @ProductName, @Price, @Quantity)";
                parameters = new SqlParameter[] {
                    new SqlParameter("@Barcode", txtBarcode.Text),
                    new SqlParameter("@ProductName", txtProductName.Text),
                    new SqlParameter("@Price", txtPrice.Text),
                    new SqlParameter("@Quantity", txtQuantity.Text)
                };
            }
            else
            {
                query = "UPDATE Products SET Barcode=@Barcode, ProductName=@ProductName, Price=@Price, Quantity=@Quantity WHERE ProductID=@ProductID";
                parameters = new SqlParameter[] {
                    new SqlParameter("@Barcode", txtBarcode.Text),
                    new SqlParameter("@ProductName", txtProductName.Text),
                    new SqlParameter("@Price", txtPrice.Text),
                    new SqlParameter("@Quantity", txtQuantity.Text),
                    new SqlParameter("@ProductID", productId)
                };
            }

            _sqlHelper.ExecuteNonQueryWithParams(query, parameters);
            this.Close();
        }
    }
}
