using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Imobiliario.DataLayer.Models;

namespace Imobiliario.DataLayer
{
    public partial class Imobiliario
    {
        public User Login(string username, string password)
        {
            User user = null;

            // [ALTERADO] Adicionamos o LEFT JOIN para trazer o OwnerID da outra tabela
            string sql = @"SELECT u.*, o.OwnerID 
                   FROM Users u 
                   LEFT JOIN Owner o ON u.UserID = o.UserID 
                   WHERE u.Username = @User AND u.PasswordHash = @Pass";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@User", username);
                cmd.Parameters.AddWithValue("@Pass", password);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString(),

                            // [NOVO] Verifica se o OwnerID existe (se não for nulo, converte)
                            OwnerID = reader["OwnerID"] != DBNull.Value
                                      ? (int?)Convert.ToInt32(reader["OwnerID"])
                                      : null
                        };
                    }
                }
                catch (Exception) { throw; }
            }
            return user;
        }

        public bool AddUser(User user)
        {
            if (string.IsNullOrEmpty(user.Role)) user.Role = "Client";

            string sql = "INSERT INTO Users (Username, PasswordHash, Role) VALUES (@User, @Pass, @Role)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@User", user.Username);
                cmd.Parameters.AddWithValue("@Pass", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", user.Role);

                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }
        public List<User> GetAllUsers()
        {
            List<User> lista = new List<User>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString()
                        });
                    }
                }
                catch (Exception) { throw; }
            }
            return lista;
        }
        public User GetUser(int id)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Users WHERE UserID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                }
                catch (Exception) { throw; }
            }
            return user;
        }
        public bool UpdateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Permitimos mudar a password e o role
                string sql = "UPDATE Users SET Username=@User, PasswordHash=@Pass, Role=@Role WHERE UserID=@ID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", user.UserID);
                cmd.Parameters.AddWithValue("@User", user.Username);
                cmd.Parameters.AddWithValue("@Pass", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", user.Role ?? "User");

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Users WHERE UserID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}