using LibraryDatabaseWPF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace LibraryDatabaseWPF
{
    public class SqlUserRepository : IUsersRepository
    {
        private readonly string connectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlUserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userAddress"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Edit a user by ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="userAddress"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        public void EditUserById(int userId, string userName, string userAddress, string phoneNumber, string email)
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
                    using (var command = new SqlCommand("Library.EditUserById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Name", userName);
                        command.Parameters.AddWithValue("Address", userAddress);
                        command.Parameters.AddWithValue("PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("Email", email);
                        command.Parameters.AddWithValue("UserId", userId);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //return new Users(userId, userName, 0, phoneNumber, email, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Get a list of Users that are renting a certain book
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public IReadOnlyList<Users> FetchUserRentingBookByISBN(string isbn)
        {
            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.FetchUsersRentingBookByISBN", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("ISBN", isbn);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        using (var reader = command.ExecuteReader())
                            return TranslateUsers(reader);
                    }
                }
            }
        }

        private Users TranslateUser(SqlDataReader reader)
        {
            var userIdOrdinal = reader.GetOrdinal("UserId");
            var userNameOrdinal = reader.GetOrdinal("Address");
            var totalCheckoutsOrdinal = reader.GetOrdinal("TotalCheckouts");
            var userAddressOrdinal = reader.GetOrdinal("Address");
            var phoneNumberOrdinal = reader.GetOrdinal("PhoneNumber");
            var emailOrdinal = reader.GetOrdinal("Email");
            var lateReturnsOrdinal = reader.GetOrdinal("LateReturns");


            if (!reader.Read())
                return null;

            return new Users(
               reader.GetInt32(userIdOrdinal),
               reader.GetString(userNameOrdinal),
               reader.GetInt32(totalCheckoutsOrdinal),
               reader.GetString(phoneNumberOrdinal),
               reader.GetString(emailOrdinal),
               reader.GetInt32(lateReturnsOrdinal));
        }

        private IReadOnlyList<Users> TranslateUsers(SqlDataReader reader)
        {
            var users = new List<Users>();

            var userIdOrdinal = reader.GetOrdinal("UserId");
            var userNameOrdinal = reader.GetOrdinal("Address");
            var totalCheckoutsOrdinal = reader.GetOrdinal("TotalCheckouts");
            var userAddressOrdinal = reader.GetOrdinal("Address");
            var phoneNumberOrdinal = reader.GetOrdinal("PhoneNumber");
            var emailOrdinal = reader.GetOrdinal("Email");
            var lateReturnsOrdinal = reader.GetOrdinal("LateReturns");

            while (reader.Read())
            {
               users.Add(new Users(
               reader.GetInt32(userIdOrdinal),
               reader.GetString(userNameOrdinal),
               reader.GetInt32(totalCheckoutsOrdinal),
               reader.GetString(phoneNumberOrdinal),
               reader.GetString(emailOrdinal),
               reader.GetInt32(lateReturnsOrdinal)));
            }

            return users;
        }

    }
}