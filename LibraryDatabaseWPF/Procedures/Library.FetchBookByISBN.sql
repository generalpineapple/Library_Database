/**************************************
 * Procedure to fetch books by a given author
 **************************************/
DROP PROCEDURE IF EXISTS [Library].FetchBookByISBN
GO

CREATE PROCEDURE [Library].FetchBooksByAuthor
   @Author NVARCHAR(256)
AS

SELECT B.BookId, B.ISBN, B.Title, B.GenreName, B.ConditionType 
FROM [Library].Books B
INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
WHERE A.AuthorName = @Author
ORDER BY B.Title ASC;

GO