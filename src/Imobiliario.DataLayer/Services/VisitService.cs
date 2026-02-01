using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Imobiliario.DataLayer.Models;

namespace Imobiliario.DataLayer
{
    public partial class Imobiliario
    {
        public bool AddVisit(Visit visit)
        {
            string sql = "INSERT INTO Visit (VisitDate, PropertyID, ClientID) VALUES (@Date, @PropID, @ClientID)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Date", visit.VisitDate);
                cmd.Parameters.AddWithValue("@PropID", visit.PropertyID);
                cmd.Parameters.AddWithValue("@ClientID", visit.ClientID);

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

        public List<Visit> GetAllVisits()
        {
            List<Visit> lista = new List<Visit>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Visit";
                SqlCommand cmd = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new Visit
                        {
                            VisitID = Convert.ToInt32(reader["VisitID"]),
                            VisitDate = Convert.ToDateTime(reader["VisitDate"]),
                            PropertyID = Convert.ToInt32(reader["PropertyID"]),
                            ClientID = Convert.ToInt32(reader["ClientID"])
                        });
                    }
                }
                catch (Exception) { throw; }
            }
            return lista;
        }

        public List<Visit> GetVisitsByProperty(int idProperty)
        {
            List<Visit> visits = new List<Visit>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Visit WHERE PropertyId = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", idProperty);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Visit visit = new Visit
                        {
                            VisitID = Convert.ToInt32(reader["VisitID"]),
                            VisitDate = Convert.ToDateTime(reader["VisitDate"]),
                            PropertyID = Convert.ToInt32(reader["PropertyID"]),
                            ClientID = Convert.ToInt32(reader["ClientID"])
                        };
                        visits.Add(visit);
                    }
                }
                catch (Exception) { throw; }
            }
            return visits;
        }
        public bool DeleteVisit(int visitId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Visit WHERE VisitID = @ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", visitId);
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