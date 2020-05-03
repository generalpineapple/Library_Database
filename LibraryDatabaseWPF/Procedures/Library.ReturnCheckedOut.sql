/****
   A user returning a book, will throw an error if the book is not checked out
**/
DROP PROCEDURE IF EXISTS [Library].ReturnCheckedOut
GO

CREATE PROCEDURE [Library].ReturnCheckedOut
   @TransactionId INT,
   @BookId INT
AS

BEGIN TRY
	UPDATE Library.CheckedOut
	SET ReturnedDate = GETDATE(), 		
	WHERE TransactionId = @TransactionId OR BookId = @BookId;

	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No book with Id %d exists.', @UserId);
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when attempting to return a book: ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO