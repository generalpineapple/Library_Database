/****
   Updating the quality of a book by BookId, will throw an error if the book doesn't exist
**/
DROP PROCEDURE IF EXISTS [Library].EditBookQuality
GO

CREATE PROCEDURE [Library].EditBookQuality
   @BookId INT,
   @conditionId INT
AS
BEGIN TRY	
	UPDATE Library.Books
	SET ConditionId = @conditionId 		
	WHERE BookId = @BookId;

	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No book with Id %d exists.', @BookId);
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when attempting to update a book: ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO