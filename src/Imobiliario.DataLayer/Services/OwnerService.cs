using Imobiliario.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Imobiliario.DataLayer
{
    public partial class Imobiliario
    {
        public bool AddOwner(Owner owner)
        {
            string sql = "INSERT INTO Owner (Name, Email, Phone) VALUES (@Name, @Email, @Phone)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", owner.Name);
                cmd.Parameters.AddWithValue("@Email", owner.Email);
                cmd.Parameters.AddWithValue("@Phone", owner.Phone);

                try { conn.Open(); return cmd.ExecuteNonQuery() > 0; }
                catch (Exception) { throw; }
            }
        }
        public List<Owner> GetAllOwners()
        {
            List<Owner> lista = new List<Owner>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Owner";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Owner
                    {
                        OwnerID = Convert.ToInt32(reader["OwnerID"]),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString()
                    });
                }
            }
            return lista;
        }

        // READ (Ler Um)
        public Owner GetOwner(int id)
        {
            Owner owner = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Owner WHERE OwnerID = @OwnerID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@OwnerID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    owner = new Owner
                    {
                        OwnerID = Convert.ToInt32(reader["OwnerID"]),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString()
                    };
                }
            }
            return owner;
        }

        public bool UpdateOwner(Owner owner)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Owner SET Name=@Name, Email=@Email, Phone=@Phone WHERE OwnerID=@ID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", owner.OwnerID);
                cmd.Parameters.AddWithValue("@Name", owner.Name);
                cmd.Parameters.AddWithValue("@Email", owner.Email);
                cmd.Parameters.AddWithValue("@Phone", owner.Phone);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // DELETE (Apagar) - Adicionei este porque vais precisar para o CRUD completo
        public bool DeleteOwner(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Cuidado: Se o dono tiver casas, isto pode dar erro de Foreign Key no SQL
                string sql = "DELETE FROM Owner WHERE OwnerID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}