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
    public class SqlAuthorRepository : IAuthorRepository
    {
        private readonly string connectionString;

        public SqlAuthorRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateAuthor(string authorName)
        {
            // Verify parameters.
            if (string.IsNullOrWhiteSpace(authorName))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(authorName));

            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.CreateAuthor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("AuthorName", authorName);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        //var authorId = (int)command.Parameters["AuthorId"].Value;

                        //return new Authors(authorId, authorName);
                    }
                }
            }
        }

        /*
        private Authors TranslateAuthor(SqlDataReader reader)
        {
            var authorIdOrdinal = reader.GetOrdinal("AuthorId");
            var nameOrdinal = reader.GetOrdinal("AuthorName");

            if (!reader.Read())
                return null;

            return new Authors(
               reader.GetInt32(authorIdOrdinal),
               reader.GetString(nameOrdinal));
        }

        private IReadOnlyList<Authors> TranslateAuthors(SqlDataReader reader)
        {
            var authors = new List<Authors>();

            var authorIdOrdinal = reader.GetOrdinal("AuthorId");
            var nameOrdinal = reader.GetOrdinal("AuthorName");

            while (reader.Read())
            {
                authors.Add(new Authors(
                   reader.GetInt32(authorIdOrdinal),
                   reader.GetString(nameOrdinal)));
            }

            return authors;
        }
        */
    }
}
