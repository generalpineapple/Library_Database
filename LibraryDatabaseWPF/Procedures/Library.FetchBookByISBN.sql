/**************************************
 * Procedure to fetch books by a given ISBN
 **************************************/
DROP PROCEDURE IF EXISTS [Library].FetchBookByISBN
GO

CREATE PROCEDURE [Library].FetchBooksByISBN
   @ISBN INT
AS

BEGIN TRY
    SELECT B.BookId, B.AuthorId, B.Title, B.GenreId, B.ConditionId
    FROM [Library].Books B
    WHERE B.ISBN = @ISBN
    ORDER BY B.Title ASC;

    IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No books from ISBN %s exists.', @ISBN);
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