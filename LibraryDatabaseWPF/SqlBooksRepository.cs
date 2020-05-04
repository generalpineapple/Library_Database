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
    class SqlBooksRepository
    {
    }
    public class SqlBookRepository : IBookRepository
    {
        private readonly string connectionString;

        public SqlBookRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Authors CreateBook(string isbn, string authorName, string title, string genreName, string conditionType)
        {
            // Verify parameters.
            if (string.IsNullOrWhiteSpace(isbn))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(isbn));
            if (string.IsNullOrWhiteSpace(authorName))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(authorName));
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(genreName))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(genreName));
            if (string.IsNullOrWhiteSpace(conditionType))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(conditionType));



            // Save to database.
            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.CreateAuthor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("ISBN", isbn);
                        command.Parameters.AddWithValue("AuthorName", authorName);
                        command.Parameters.AddWithValue("Title", title);
                        command.Parameters.AddWithValue("GenreName", genreName);
                        command.Parameters.AddWithValue("ConditionType", conditionType);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        var bookId = (int)command.Parameters["BookId"].Value;


                        //return new Books(bookId, isbn, authorName, title, genreName, conditionType);
                    }
                }
            }
        }

        public Authors FetchAuthor(int authorId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Person.FetchPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("AUthorId", authorId);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        var author = TranslateAuthor(reader);

                        if (author == null)
                            throw new RecordNotFoundException(authorId.ToString());

                        return author;
                    }
                }
            }
        }

        public IReadOnlyList<Authors> RetrieveAuthors()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Person.RetrievePersons", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                        return TranslateAuthors(reader);
                }
            }
        }

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
    }
}
