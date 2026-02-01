using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Imobiliario.DataLayer.Models;

namespace Imobiliario.DataLayer
{
    public partial class Imobiliario
    {
        public bool AddSale(Sale sale)
        {
            string sql = @"
                INSERT INTO Sale (PropertyID, ClientID, FinalPrice, Date, OwnerID)
                SELECT @PropertyID, @ClientID, @FinalPrice, @Date, OwnerID
                FROM Property
                WHERE PropertyID = @PropertyID";

            // Define a data de hoje
            sale.Date = DateTime.Now;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@PropertyID", sale.PropertyID);
                cmd.Parameters.AddWithValue("@ClientID", sale.ClientID);
                cmd.Parameters.AddWithValue("@FinalPrice", sale.FinalPrice);
                cmd.Parameters.AddWithValue("@Date", sale.Date);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<Sale> GetAllSales()
        {
            List<Sale> lista = new List<Sale>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
               string sql = "SELECT * FROM Sale";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Sale
                        {
                            SaleID = Convert.ToInt32(reader["SaleID"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            FinalPrice = Convert.ToDecimal(reader["FinalPrice"]),
                            PropertyID = Convert.ToInt32(reader["PropertyID"]),
                            ClientID = Convert.ToInt32(reader["ClientID"])
                        });
                    }
                }
                catch (Exception) { throw; }
            }
            return lista;
        }

        public Sale GetSale(int id)
        {
            Sale sale = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Sale WHERE SaleID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        sale = new Sale
                        {
                            SaleID = Convert.ToInt32(reader["SaleID"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            FinalPrice = Convert.ToDecimal(reader["FinalPrice"]),
                            PropertyID = Convert.ToInt32(reader["PropertyID"]),
                            ClientID = Convert.ToInt32(reader["ClientID"])
                        };
                    }
                }
                catch (Exception) { throw; }
            }
            return sale;
        }

        public bool UpdateSale(Sale sale)
        {

            string sql = @"
                        UPDATE Sale 
                        SET Date = @Date, 
                            FinalPrice = @FinalPrice, 
                            PropertyID = @PropertyID, 
                            ClientID = @ClientID,
                            OwnerID = (SELECT OwnerID FROM Property WHERE PropertyID = @PropertyID)
                        WHERE SaleID = @SaleID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Date", sale.Date);
                cmd.Parameters.AddWithValue("@FinalPrice", sale.FinalPrice);
                cmd.Parameters.AddWithValue("@PropertyID", sale.PropertyID);
                cmd.Parameters.AddWithValue("@ClientID", sale.ClientID);

                cmd.Parameters.AddWithValue("@SaleID", sale.SaleID);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool DeleteSale(int id)
        {
            string sql = "DELETE FROM Sale WHERE SaleID = @ID";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
    }
}