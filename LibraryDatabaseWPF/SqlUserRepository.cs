using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using LibraryDatabaseWPF.Models;

namespace LibraryDatabaseWPF
{
    public class SqlUserRepository : IUsersRepository
    {
        private readonly string connectionString;

        public SqlUserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Users CreateUser(string userName, string userAddress, string phoneNumber, string email)
        {
            // Verify parameters.
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(userName));
            if (string.IsNullOrWhiteSpace(userAddress))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(userAddress));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(phoneNumber));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(email));


            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.CreateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Name", userName);
                        command.Parameters.AddWithValue("Address", userAddress);
                        command.Parameters.AddWithValue("PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("Email", email);


                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        var userId = (int)command.Parameters["AuthorId"].Value;

                        return new Users(userId, userName, 0, phoneNumber, email, 0);
                    }
                }
            }
        }

    }
