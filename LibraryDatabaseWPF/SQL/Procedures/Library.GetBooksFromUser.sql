/**************************************
 * Procedure to list books checked out by a given user
 **************************************/
DROP PROCEDURE IF EXISTS [Library].GetBooksFromUser
GO

CREATE PROCEDURE [Library].GetBooksFromUser
   @Username VARCHAR(64)
AS

SELECT B.BookId, B.ISBN, B.Title, A.AuthorName, Ch.CheckoutDate
FROM [Library].CheckedOut Ch
INNER JOIN [Library].Users U ON Ch.UserId = U.UserId
	INNER JOIN [Library].Books B ON Ch.BookId = B.BookId
		INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
WHERE U.[Name] = @Username
ORDER BY B.Title ASC;

GO