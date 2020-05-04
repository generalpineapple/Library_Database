/* QUERY 1
   List of books that need to be replaced, based on condition and popularity
 */
DROP PROCEDURE IF EXISTS [Library].FetchBooksToReplace
GO

CREATE PROCEDURE [Library].FetchBooksToReplace
   
AS

SELECT
    B.BookId,
    B.ISBN,
    B.AuthorId,
    B.Title,
    B.GenreId,
    B.ConditionId
FROM
    (
        SELECT
            BookId,
            AuthorId,
            Title,
            GenreName
        FROM
            Library.Books
        WHERE
            ConditionType = 'Replace'
    ) B
ORDER BY
    B.BookId ASC;
	
GO
	
