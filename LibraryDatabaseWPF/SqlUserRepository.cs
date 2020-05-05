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
        public void CreateUser(string userName, string userAddress, string phoneNumber, string email)
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

                        //var userId = (int)command.Parameters["UserId"].Value;

                        //return new Users(userId, userName, 0, phoneNumber, email, 0);
                    }
                }
            }
        }

        public IReadOnlyList<Users> FetchAllUsers()
        {
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.FetchAllUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        //command.ExecuteNonQuery();

                        //transaction.Complete();

                        using (var reader = command.ExecuteReader())
                            return TranslateUsers(reader);
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

                        //command.ExecuteNonQuery();

                        //transaction.Complete();

                        using (var reader = command.ExecuteReader())
                            return TranslateUsers(reader);
                    }
                }
            }
        }

        /// <summary>
        /// Get a list of top users
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Users> GetTopUsers()
        {
            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.GetTopUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        //command.ExecuteNonQuery();

                       // transaction.Complete();

                        using (var reader = command.ExecuteReader())
                            return TranslateUsers(reader);
                    }
                }
            }
        }
        public IReadOnlyList<Users> FetchUserByName(string name)
        {
            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.FetchUserByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("Name", name);

                        connection.Open();

                        //command.ExecuteNonQuery();

                        // transaction.Complete();

                        using (var reader = command.ExecuteReader())
                            return TranslateUsers(reader);
                    }
                }
            }
        }

        public UserReport CreateUserReport(int userId)
        {
            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.CreateUserReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("UserId", userId);

                        connection.Open();

                        //command.ExecuteNonQuery();

                        //transaction.Complete();

                        using (var reader = command.ExecuteReader())
                            return TranslateUserReport(reader);
                    }
                }
            }
        }
        private UserReport TranslateUserReport(SqlDataReader reader)
        {
            //public UserReport(int currentCheckouts, int onTimeReturns, int lateReturns, int overDueBooks, int daysLate)

            var nameOrdinal = reader.GetOrdinal("Name");
            var userIdOrdinal = reader.GetOrdinal("UserId");
            var totalCheckoutsOrdinal = reader.GetOrdinal("TotalCheckOuts");
            var onTimeReturnsOrdinal = reader.GetOrdinal("OnTimeReturns");
            var lateReturnsOrdinal = reader.GetOrdinal("LateReturns");
            var overDueBooksOrdinal = reader.GetOrdinal("OverdueBooks");
            var lateFeesOrdinal = reader.GetOrdinal("LateFees");

            if (!reader.Read())
                return null;

            string name = reader.GetString(nameOrdinal);
            int id = reader.GetInt32(userIdOrdinal);
            int checkouts = reader.GetInt32(totalCheckoutsOrdinal);

            int ontime; 
            if (!reader.IsDBNull(onTimeReturnsOrdinal))
            {
                ontime = reader.GetInt32(onTimeReturnsOrdinal);
            }
            else
            {
                ontime = 0;
            }

            int late = reader.GetInt32(lateReturnsOrdinal);

            int overdue;
            if (!reader.IsDBNull(overDueBooksOrdinal))
            {
                overdue = reader.GetInt32(overDueBooksOrdinal);
            }
            else
            {
                overdue = 0;
            }

            int fees;
            if (!reader.IsDBNull(lateFeesOrdinal))
            {
                fees = reader.GetInt32(lateFeesOrdinal);
            }
            else
            {
                fees = 0;
            }

            return new UserReport(
               name,
               id,
               checkouts,
               ontime,
               late,
               overdue,
               fees);
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
            var userNameOrdinal = reader.GetOrdinal("Name");
            var totalCheckoutsOrdinal = reader.GetOrdinal("TotalCheckouts");
            //var userAddressOrdinal = reader.GetOrdinal("Address");
            var phoneNumberOrdinal = reader.GetOrdinal("PhoneNumber");
            var emailOrdinal = reader.GetOrdinal("Email");
            var lateReturnsOrdinal = reader.GetOrdinal("LateReturns");

            while (reader.Read())
            {
                users.Add(
                    new Users(
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