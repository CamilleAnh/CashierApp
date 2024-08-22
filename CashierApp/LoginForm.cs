using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CashierApp
{
    public partial class LoginForm : Form
    {
        private SQLHelper sqlHelper;

        public LoginForm()
        {
            InitializeComponent();
            sqlHelper = new SQLHelper(); // Khởi tạo SQLHelper
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // SQL query with parameters to check credentials
            string query = "SELECT COUNT(1) FROM Users WHERE Username=@Username AND PasswordHash=@PasswordHash";

            SqlParameter[] parameters = {
                new SqlParameter("@Username", username),
                new SqlParameter("@PasswordHash", password) // Không mã hóa mật khẩu
            };

            try
            {
                int count = Convert.ToInt32(sqlHelper.ExecuteScalarWithParams(query, parameters));

                if (count == 1)
                {
                    MainForm mainForm = new MainForm();
                    this.Hide();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng nhập: {ex.Message}");
            }
        }

        

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Bất kỳ logic khởi tạo nào có thể thêm vào đây
        }
    }
}
