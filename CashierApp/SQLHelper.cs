using System;
using System.Data;
using System.Data.SqlClient;

namespace CashierApp
{
    public class SQLHelper
    {
        private readonly string connectionString;

        // Khởi tạo với chuỗi kết nối
        public SQLHelper()
        {
            // Sử dụng chuỗi kết nối của bạn ở đây
            connectionString = "Server=LAPTOP-1JHTGG11;Database=CashierAppDB;User Id=sa;Password=123456;";
        }

        public object ExecuteScalarWithParams(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                try
                {
                    connection.Open();  // Đảm bảo kết nối được mở
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Log lỗi hoặc thông báo lỗi ở đây nếu cần
                    throw new ApplicationException("Error executing query with parameters.", ex);
                }
            }
            return dataTable;
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        // Thực hiện truy vấn không có tham số
        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                try
                {
                    connection.Open();  // Đảm bảo kết nối được mở
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Log lỗi hoặc thông báo lỗi ở đây nếu cần
                    throw new ApplicationException("Error executing query.", ex);
                }
            }
            return dataTable;
        }

        // Thực hiện truy vấn với tham số
        public DataTable ExecuteQueryWithParams(string query, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                try
                {
                    connection.Open();  // Đảm bảo kết nối được mở
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Log lỗi hoặc thông báo lỗi ở đây nếu cần
                    throw new ApplicationException("Error executing query with parameters.", ex);
                }
            }
            return dataTable;
        }

        // Thực hiện câu lệnh không truy vấn không có tham số
        public int ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();  // Đảm bảo kết nối được mở
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log lỗi hoặc thông báo lỗi ở đây nếu cần
                    throw new ApplicationException("Error executing non-query.", ex);
                }
            }
        }

    
    // Thực hiện câu lệnh không truy vấn với tham số
    public int ExecuteNonQueryWithParams(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddRange(parameters);
                try
                {
                    connection.Open();  // Đảm bảo kết nối được mở
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log lỗi hoặc thông báo lỗi ở đây nếu cần
                    throw new ApplicationException("Error executing non-query with parameters.", ex);
                }
            }
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}