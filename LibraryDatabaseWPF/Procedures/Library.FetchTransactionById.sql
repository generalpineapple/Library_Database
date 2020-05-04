/*** Returns the Transaction associated with the given Id
****/
DROP PROCEDURE IF EXISTS [Library].FetchTransactionById
GO

CREATE PROCEDURE [Library].FetchTransactionById
@TransactionId INT
AS

BEGIN TRY
	SELECT C.BookId, C.UserId, C.CheckoutDate, C.ReturnedDate 
	FROM Library.CheckedOut C
	WHERE C.TransactionId = @TransactionId
	
	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No checkout with Id %d exists.', @BookId);
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