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
    public class SqlAuthorsRepository : IAuthorRepository
    {
        private readonly string connectionString;

        public SqlAuthorsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Authors CreateAuthor(string name)
        {
            // Verify parameters.
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(name));

            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.CreateAuthor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("AuthorName", name);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        var authorId = (int)command.Parameters["AuthorId"].Value;

                        return new Authors(authorId, name);
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
