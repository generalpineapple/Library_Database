/**************************************
 * Procedure to fetch books by a given ISBN
 **************************************/
DROP PROCEDURE IF EXISTS [Library].FetchBookByISBN
GO

CREATE PROCEDURE [Library].FetchBooksByISBN
   @ISBN INT
AS

    SELECT B.BookId, B.AuthorId, B.Title, B.GenreId, B.ConditionId
    FROM [Library].Books B
    WHERE B.ISBN = @ISBN
    ORDER BY B.Title ASC;

GO