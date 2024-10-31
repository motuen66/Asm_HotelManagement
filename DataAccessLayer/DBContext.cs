﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public abstract class DBContext
    {
        public static SqlConnection getConnectionString()
        {
            string _connectionString = "Data Source=LAPTOP-MSLN4BE5;Initial Catalog=FUMiniHotelManagement;User Id=sa;Password=12345;";
            //var sqlstringBuilder = new SqlConnectStringBuilder();
            //sqlstringBuilder["Server"] = 

            using var conn = new SqlConnection(_connectionString);
            //conn.Open();
            //Console.WriteLine(conn.State);
            //conn.Close();
            return new SqlConnection (_connectionString);
        }

        // Using for INSERT, DELETE, UPDATE query
        public static int ExecuteNonQuerry(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = getConnectionString())
            {
                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    if (parameters != null) 
                    { 
                        command.Parameters.AddRange(parameters);    
                    }
                    
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        //Using for SELECT query
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = getConnectionString())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }
            return dataTable;
        }
    }
}
