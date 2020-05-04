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

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlBookRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Create a new book 
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="authorName"></param>
        /// <param name="title"></param>
        /// <param name="genreName"></param>
        /// <param name="conditionType"></param>
        /// <returns></returns>
        public Books CreateBook(string isbn, string authorName, string title, string genreName, string conditionType)
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
                        int authorId = GetAuthorIdFromName(authorName); 
                        

                        return new Books(bookId, isbn, authorId, title, genreName, conditionType);
                    }
                }
            }
        }

        /// <summary>
        /// Get author id from an author name
        /// </summary>
        /// <param name="authorName"></param>
        /// <returns></returns>
        private int GetAuthorIdFromName(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
                throw new ArgumentException("The parameter cannot be null or empty.", nameof(authorName));

            using (var transaction = new TransactionScope())
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("Library.GetAuthorIdFromName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("AuthorName", authorName);

                        connection.Open();

                        command.ExecuteNonQuery();

                        transaction.Complete();

                        using (var reader = command.ExecuteReader())
                            return reader.GetOrdinal("AuthorId");
                    }
                }
            }
        }

        private Authors TranslateBook(SqlDataReader reader)
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
