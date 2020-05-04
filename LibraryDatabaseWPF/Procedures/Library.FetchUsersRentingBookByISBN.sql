/**************************************
 * Procedure to see who has books of a certain ISBN checked out
 **************************************/
DROP PROCEDURE IF EXISTS [Library].FetchUsersRentingBookByISBN
GO

CREATE PROCEDURE [Library].FetchUsersRentingBookByISBN
   @ISBN INT
AS

BEGIN TRY
    SELECT U.UserId, U.[Name], U.PhoneNumber
    FROM [Library].CheckedOut Ch
    INNER JOIN [Library].Users U ON Ch.UserId = U.UserId
	    INNER JOIN [Library].Books B ON Ch.BookId = B.BookId
    WHERE B.ISBN = @ISBN
    ORDER BY U.Name ASC;

    IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No books from ISBN %d exists.', @ISBN);
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when attempting to find books ' +
         ERROR_MESSAGE();         
   PRINT @ErrorMessage;
   THROW; -- Rethrow.
END CATCH;
GO

