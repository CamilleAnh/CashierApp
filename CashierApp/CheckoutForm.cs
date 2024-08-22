using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CashierApp
{
    public partial class CheckoutForm : Form
    {
        public CheckoutForm()
        {
            InitializeComponent();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (txtBarcode.Text.Length > 0)
            {
                using (SqlConnection conn = new SqlConnection("your_connection_string"))
                {
                    conn.Open();
                    string query = "SELECT * FROM Products WHERE Barcode=@Barcode";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Barcode", txtBarcode.Text);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        dgvCart.Rows.Add(reader["ProductID"], reader["ProductName"], 1, reader["Price"], reader["Price"]);
                    }
                }
                txtBarcode.Clear();
                CalculateTotal();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow != null)
            {
                dgvCart.Rows.Remove(dgvCart.CurrentRow);
                CalculateTotal();
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("your_connection_string"))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string query = "INSERT INTO Orders (OrderDate, TotalAmount) OUTPUT INSERTED.OrderID VALUES (@OrderDate, @TotalAmount)";
                    SqlCommand cmd = new SqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TotalAmount", lblTotal.Text);
                    int orderId = (int)cmd.ExecuteScalar();

                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        string productId = row.Cells["ProductID"].Value.ToString();
                        string quantity = row.Cells["Quantity"].Value.ToString();
                        string price = row.Cells["Price"].Value.ToString();

                        query = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price) VALUES (@OrderID, @ProductID, @Quantity, @Price)";
                        cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@OrderID", orderId);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.ExecuteNonQuery();

                        query = "UPDATE Products SET Quantity = Quantity - @Quantity WHERE ProductID=@ProductID";
                        cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Thanh toán thành công!");
                    dgvCart.Rows.Clear();
                    lblTotal.Text = "0";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Total"].Value);
            }
            lblTotal.Text = total.ToString("C");
        }
    }
}
