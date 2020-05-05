using LibraryDatabaseWPF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace LibraryDatabaseWPF
{
    public class SqlInventoryRepository : IInventoryRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlInventoryRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateInventory(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(isbn));

            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.CreateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("ISBN", isbn);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //var userId = (int)command.Parameters["UserId"].Value;

                        //return GetInventoryByISBN(isbn);
                    }
                }
            }
        }

        public Inventory GetInventoryByISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(isbn));

            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.GetInventoryByISBN", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("ISBN", isbn);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        using (var reader = command.ExecuteReader())
                            return TranslateInventory(reader);
                    }
                }
            }
        }

        private Inventory TranslateInventory(SqlDataReader reader)
        {
            var isbnOrdinal = reader.GetOrdinal("ISBN");
            var totalCopiesOrdinal = reader.GetOrdinal("TotalCopies");
            var totalCheckoutsOrdinal = reader.GetOrdinal("TotalCheckouts");

            if (!reader.Read())
                return null;

            return new Inventory(
               reader.GetString(isbnOrdinal),
               reader.GetInt32(totalCopiesOrdinal),
               reader.GetInt32(totalCheckoutsOrdinal));
        }

    }
}