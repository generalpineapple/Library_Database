/**************************************
 * Procedure to see who has books of a certain ISBN checked out
 **************************************/
DROP PROCEDURE IF EXISTS [Library].FetchUsersRentingBookByISBN
GO

CREATE PROCEDURE [Library].FetchUsersRentingBookByISBN
   @ISBN INT
AS

SELECT U.UserId, U.[Name], U.PhoneNumber
FROM [Library].CheckedOut Ch
INNER JOIN [Library].Users U ON Ch.UserId = U.UserId
	INNER JOIN [Library].Books B ON Ch.BookId = B.BookId
WHERE B.ISBN = @ISBN
ORDER BY U.Name ASC;

GO
