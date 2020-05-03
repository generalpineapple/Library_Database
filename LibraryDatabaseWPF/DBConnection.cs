﻿using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Windows; 

namespace LibraryDatabaseWPF
{
    public class DBConnection
    {
        // sql command
        public static SqlCommand sqlCommand;
        // sql connection 
        public static SqlConnection sqlConnection;


        // Holds username
        public static string username = ConfigurationManager.AppSettings["username"];
        // Holds password
        public static string password = ConfigurationManager.AppSettings["password"];

        // connection string
        public static string connectionString = $"Server=mssql.cs.ksu.edu;Database={username};User Id={username};Password={password};";

        /// <summary>
        /// Open a connection to the database
        /// </summary>
        static void openConnection()
        {
            sqlConnection = new SqlConnection(connectionString); 

            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.ConnectionString = connectionString;
                    sqlConnection.Open(); 

                }  
            }
            catch (Exception e)
            {
                MessageBox.Show("IDK some error?" + Environment.NewLine + e.ToString());
            }

        }

        /// <summary>
        /// Close connection to the database
        /// </summary>
        static void closeConnection()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close(); 
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("IDK some error?" + Environment.NewLine + e.ToString());
            }
        }

        public static void QueryData(string queryString)
        {
            // set the sql query string
            sqlCommand = new SqlCommand(queryString, sqlConnection);

            openConnection();

            sqlCommand.ExecuteNonQuery();

            closeConnection(); 
        }

       
    }
}