using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CashierApp
{
    public partial class UserManagementForm : Form
    {
        public UserManagementForm()
        {
            InitializeComponent();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
            usersGridView.Columns["PasswordHash"].Visible = false; // Hide the password hash column
            usersGridView.Columns["UserID"].HeaderText = "Mã Người Dùng";
            usersGridView.Columns["Username"].HeaderText = "Tên Đăng Nhập";
            usersGridView.Columns["FullName"].HeaderText = "Họ Tên";
            usersGridView.Columns["Role"].HeaderText = "Vai Trò";
        }

        private void LoadUsers()
        {
            string query = "SELECT * FROM Users";
            SQLHelper sqlHelper = new SQLHelper();
            DataTable usersTable = sqlHelper.ExecuteQuery(query);
            usersGridView.DataSource = usersTable;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO Users (Username, PasswordHash, FullName, Role) VALUES (@Username, @PasswordHash, @FullName, @Role)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", usernameTextBox.Text),
                new SqlParameter("@PasswordHash", HashPassword(passwordTextBox.Text)),
                new SqlParameter("@FullName", fullNameTextBox.Text),
                new SqlParameter("@Role", roleTextBox.Text)
            };
            SQLHelper sqlHelper = new SQLHelper();
            sqlHelper.ExecuteNonQuery(query, parameters);
            LoadUsers();
            ClearInputFields();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (usersGridView.SelectedRows.Count > 0)
            {
                int selectedUserID = Convert.ToInt32(usersGridView.SelectedRows[0].Cells["UserID"].Value);
                string query = "UPDATE Users SET Username = @Username, FullName = @FullName, Role = @Role WHERE UserID = @UserID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", selectedUserID),
                    new SqlParameter("@Username", usernameTextBox.Text),
                    new SqlParameter("@FullName", fullNameTextBox.Text),
                    new SqlParameter("@Role", roleTextBox.Text)
                };
                SQLHelper sqlHelper = new SQLHelper();
                sqlHelper.ExecuteNonQuery(query, parameters);
                LoadUsers();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người dùng để chỉnh sửa.");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (usersGridView.SelectedRows.Count > 0)
            {
                int selectedUserID = Convert.ToInt32(usersGridView.SelectedRows[0].Cells["UserID"].Value);
                string query = "DELETE FROM Users WHERE UserID = @UserID";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", selectedUserID)
                };
                SQLHelper sqlHelper = new SQLHelper();
                sqlHelper.ExecuteNonQuery(query, parameters);
                LoadUsers();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người dùng để xóa.");
            }
        }

        private void usersGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = usersGridView.Rows[e.RowIndex];
                usernameTextBox.Text = row.Cells["Username"].Value.ToString();
                fullNameTextBox.Text = row.Cells["FullName"].Value.ToString();
                roleTextBox.Text = row.Cells["Role"].Value.ToString();
            }
        }

        private void ClearInputFields()
        {
            usernameTextBox.Clear();
            passwordTextBox.Clear();
            fullNameTextBox.Clear();
            roleTextBox.Clear();
        }

        private string HashPassword(string password)
        {
            // Implement password hashing here
            return password; // Replace this with actual hashing logic
        }
    }
}
