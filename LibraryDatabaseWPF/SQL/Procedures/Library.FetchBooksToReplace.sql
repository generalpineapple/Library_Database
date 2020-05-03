/* QUERY 1
   List of books that need to be replaced, based on condition and popularity
 */
DROP PROCEDURE IF EXISTS [Library].FetchBooksToReplace
GO

CREATE PROCEDURE [Library].FetchBooksToReplace
   
AS

SELECT
    B.BookId,
    B.AuthorId,
    B.Title,
    B.GenreName,
    A.AuthorName
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
    INNER JOIN
        Library.Authors A ON A.AuthorId = B.AuthorId
ORDER BY
    B.BookId ASC;
	
GO
	
