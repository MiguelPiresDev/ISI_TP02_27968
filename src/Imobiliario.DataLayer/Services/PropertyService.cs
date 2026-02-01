using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Imobiliario.DataLayer.Models;
using Imobiliario.DataLayer.Contract;

namespace Imobiliario.DataLayer
{
    public partial class Imobiliario : IImobiliario
    {
        // 1. CRIAR (INSERT)
        public bool AddProperty(Property p)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Atenção aos espaços no fim das linhas das strings SQL
                string sql = "INSERT INTO Property (Name, Address, Price, Area, Description, Type, YearBuilt, State, OwnerID, Latitude, Longitude) " +
                             "VALUES (@Name, @Address, @Price, @Area, @Description, @Type, @YearBuilt, @State, @OwnerID, @Latitude, @Longitude)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                // Parâmetros Obrigatórios
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Address", p.Address);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Area", p.Area);
                cmd.Parameters.AddWithValue("@Description", p.Description ?? (object)DBNull.Value); // Proteção contra nulos
                cmd.Parameters.AddWithValue("@Type", p.Type);
                cmd.Parameters.AddWithValue("@YearBuilt", p.YearBuilt);
                cmd.Parameters.AddWithValue("@OwnerID", p.OwnerID);
                cmd.Parameters.AddWithValue("@State", p.State ? 1 : 0);
                cmd.Parameters.AddWithValue("@Latitude", (object)p.Latitude ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Longitude", (object)p.Longitude ?? DBNull.Value);

                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw;}
            }
        }

        public List<Property> GetAllProperties()
        {
            List<Property> lista = new List<Property>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Property";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Property p = new Property();

                        // Conversões Seguras
                        p.PropertyID = Convert.ToInt32(reader["PropertyID"]);
                        p.Name = reader["Name"].ToString();
                        p.Address = reader["Address"].ToString();
                        p.Price = Convert.ToDecimal(reader["Price"]);
                        p.Area = Convert.ToDouble(reader["Area"]);
                        p.Description = reader["Description"].ToString();
                        p.Type = reader["Type"].ToString();
                        p.YearBuilt = Convert.ToInt32(reader["YearBuilt"]);
                        p.State = Convert.ToBoolean(reader["State"]);
                        p.OwnerID = Convert.ToInt32(reader["OwnerID"]);

                        // Verifica nulos do Mapa
                        if (reader["Latitude"] != DBNull.Value)
                            p.Latitude = Convert.ToDouble(reader["Latitude"]);

                        if (reader["Longitude"] != DBNull.Value)
                            p.Longitude = Convert.ToDouble(reader["Longitude"]);

                        lista.Add(p);
                    }
                }
                catch (Exception) { throw; }
            }
            return lista;
        }

        // 3. OBTER UM (SELECT BY ID)
        public Property GetProperty(int id)
        {
            Property p = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Property WHERE PropertyID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    p = new Property();
                    p.PropertyID = Convert.ToInt32(reader["PropertyID"]);
                    p.Name = reader["Name"].ToString();
                    p.Address = reader["Address"].ToString();
                    p.Price = Convert.ToDecimal(reader["Price"]);
                    p.Area = Convert.ToDouble(reader["Area"]);
                    p.Description = reader["Description"].ToString();
                    p.Type = reader["Type"].ToString();
                    p.YearBuilt = Convert.ToInt32(reader["YearBuilt"]);
                    p.State = Convert.ToBoolean(reader["State"]);
                    p.OwnerID = Convert.ToInt32(reader["OwnerID"]);

                    if (reader["Latitude"] != DBNull.Value) p.Latitude = Convert.ToDouble(reader["Latitude"]);
                    if (reader["Longitude"] != DBNull.Value) p.Longitude = Convert.ToDouble(reader["Longitude"]);
                }
            }
            return p;
        }

        // 4. ATUALIZAR (UPDATE)
        public bool UpdateProperty(Property p)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Padronizei os nomes (@Description em vez de @Desc e @YearBuilt em vez de @Year)
                string sql = "UPDATE Property SET Name=@Name, Address=@Address, Price=@Price, Area=@Area, Description=@Description, " +
                             "Type=@Type, YearBuilt=@YearBuilt, State=@State OwnerID=@OwnerID, Latitude=@Lat, Longitude=@Lon " +
                             "WHERE PropertyID=@ID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Address", p.Address);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Area", p.Area);
                cmd.Parameters.AddWithValue("@Description", p.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Type", p.Type);
                cmd.Parameters.AddWithValue("@YearBuilt", p.YearBuilt);
                cmd.Parameters.AddWithValue("@State", p.State ? 1 : 0);
                cmd.Parameters.AddWithValue("@OwnerID", p.OwnerID);
                cmd.Parameters.AddWithValue("@Lat", (object)p.Latitude ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Lon", (object)p.Longitude ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ID", p.PropertyID);

                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception) { throw; }
            }
        }

        public bool DeleteProperty(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Property WHERE PropertyID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                try { conn.Open(); return cmd.ExecuteNonQuery() > 0; }
                catch (Exception) { throw; }
            }
        }
    }
}