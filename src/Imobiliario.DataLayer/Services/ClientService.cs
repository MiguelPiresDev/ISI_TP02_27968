using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Imobiliario.DataLayer.Models;

namespace Imobiliario.DataLayer
{
    public partial class Imobiliario
    {
        public bool AddClient(Client client)
        {
            string sql = "INSERT INTO Client (Name, Email, Phone) VALUES (@Name, @Email, @Phone)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Name", client.Name);
                cmd.Parameters.AddWithValue("@Email", client.Email);
                cmd.Parameters.AddWithValue("@Phone", client.Phone);

                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }

        // 2. LISTAR TODOS (GetAllClients)
        public List<Client> GetAllClients()
        {
            List<Client> lista = new List<Client>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Client";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Client
                        {
                            ClientID = Convert.ToInt32(reader["ClientID"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                        });
                    }
                }
                catch (Exception) { throw; }
            }
            return lista;
        }

        // 3. OBTER UM CLIENTE (GetClient)
        public Client GetClient(int id)
        {
            Client client = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Client WHERE ClientID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        client = new Client
                        {
                            ClientID = Convert.ToInt32(reader["ClientID"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                        };
                    }
                }
                catch (Exception) { throw; }
            }
            return client;
        }

        public bool UpdateClient(Client client)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Client SET Name=@Name, Email=@Email, Phone=@Phone WHERE ClientID=@ID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", client.ClientID);
                cmd.Parameters.AddWithValue("@Name", client.Name);
                cmd.Parameters.AddWithValue("@Email", client.Email);
                cmd.Parameters.AddWithValue("@Phone", client.Phone);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteClient(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Cuidado: Se o cliente tiver vendas ou visitas, isto pode dar erro de FK
                string sql = "DELETE FROM Client WHERE ClientID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}