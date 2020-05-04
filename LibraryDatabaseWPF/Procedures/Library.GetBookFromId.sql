/*** Returns the book associated with the given BookId
****/
DROP PROCEDURE IF EXISTS [Library].GetBookFromId
GO

CREATE PROCEDURE [Library].GetBookFromId
@BookId INT
AS

BEGIN TRY
	SELECT B.ISBN, B.AuthorId, B.Title, B.GenreId, B.ConditionId
	FROM Library.Books B
	WHERE B.BookId = @BookId
	
	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No book with Id %d exists.', @AuthorId);
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when attempting to find the book: ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO