using System;
using System.Data.SqlClient;
using Imobiliario.DataLayer.Models;
using Imobiliario.DataLayer.Contract;

namespace Imobiliario.DataLayer
{
    public partial class Imobiliario : IImobiliario
    {
        public bool RegisterUser(RegisterData data)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Usamos transação porque vamos gravar em 2 tabelas
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string sqlUser = "INSERT INTO Users (Username, PasswordHash, Role) " +
                                     "VALUES (@User, @Pass, @Role); " +
                                     "SELECT CAST(scope_identity() AS int)";

                    SqlCommand cmdUser = new SqlCommand(sqlUser, conn, transaction);
                    cmdUser.Parameters.AddWithValue("@User", data.Username);
                    cmdUser.Parameters.AddWithValue("@Pass", data.Password);
                    cmdUser.Parameters.AddWithValue("@Role", data.Role);

                    // Executa e guarda o ID do novo User
                    int newUserId = (int)cmdUser.ExecuteScalar();

                    string sqlProfile = "";

                    if (data.Role == "Owner")
                    {
                        sqlProfile = "INSERT INTO Owner (Name, Phone, Email, UserID) " +
                                     "VALUES (@Name, @Phone, @Email, @UserID)";
                    }
                    else
                    {
                        sqlProfile = "INSERT INTO Client (Name, Phone, Email, UserID) " +
                                     "VALUES (@Name, @Phone, @Email, @UserID)";
                    }

                    SqlCommand cmdProfile = new SqlCommand(sqlProfile, conn, transaction);
                    cmdProfile.Parameters.AddWithValue("@Name", data.Name);
                    cmdProfile.Parameters.AddWithValue("@Phone", data.Phone);
                    cmdProfile.Parameters.AddWithValue("@Email", data.Email);
                    cmdProfile.Parameters.AddWithValue("@UserID", newUserId);

                    cmdProfile.ExecuteNonQuery();

                    // Se tudo correu bem, grava permanentemente
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    // Se deu erro, desfaz tudo (não cria user sem perfil)
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}