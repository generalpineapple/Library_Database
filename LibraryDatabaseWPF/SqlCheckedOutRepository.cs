using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using LibraryDatabaseWPF.Models;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace LibraryDatabaseWPF
{
    public class SqlCheckedOutRepository : ICheckedOutRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlCheckedOutRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public CheckedOut CreateCheckedOut(int bookId, int userId)
        {

            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.CreateCheckedOut", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("BookId", bookId);
                        command.Parameters.AddWithValue("UserId", userId);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        var transactionId = (int)command.Parameters["TransactionId"].Value;

                        //return new Transaction();
                    }
                }
            }
        }

        private CheckedOut TranslateCheckedOut(SqlDataReader reader)
        {
            var transactionIdOrdinal = reader.GetOrdinal("TransactionId");
            var bookIdOrdinal = reader.GetOrdinal("UserId");
            var userIdOrdinal = reader.GetOrdinal("Address");
            var checkoutDate = reader.GetOrdinal("CheckoutDate");
            var returnedDate = reader.GetOrdinal("returnedDate");
            var dueDate = reader.GetOrdinal("DueDate");

            if (!reader.Read())
                return null;

            return new CheckedOut(
               reader.GetInt32(transactionIdOrdinal),
               reader.GetInt32(bookIdOrdinal),
               reader.GetInt32(userIdOrdinal),
               reader.GetDateTime(checkoutDate),
               reader.GetDateTime(returnedDate),
               reader.GetDateTime(dueDate));
        }

        private IReadOnlyList<CheckedOut> CheckedOut(SqlDataReader reader)
        {
            var checkouts = new List<CheckedOut>();

            var transactionIdOrdinal = reader.GetOrdinal("TransactionId");
            var bookIdOrdinal = reader.GetOrdinal("UserId");
            var userIdOrdinal = reader.GetOrdinal("Address");
            var checkoutDate = reader.GetOrdinal("CheckoutDate");
            var returnedDate = reader.GetOrdinal("returnedDate");
            var dueDate = reader.GetOrdinal("DueDate");

            while (reader.Read())
            {
                checkouts.Add(
                    new CheckedOut(
                        reader.GetInt32(transactionIdOrdinal),
                        reader.GetInt32(bookIdOrdinal),
                        reader.GetInt32(userIdOrdinal),
                        reader.GetDateTime(checkoutDate),
                        reader.GetDateTime(returnedDate),
                        reader.GetDateTime(dueDate)));
            }

            return checkouts;
        }

    }
}
