/*** Returns the Transaction associated with the given Id
****/
DROP PROCEDURE IF EXISTS [Library].FetchTransactionById
GO

CREATE PROCEDURE [Library].FetchTransactionById
@TransactionId INT
AS


	SELECT C.BookId, C.UserId, C.CheckoutDate, C.ReturnedDate 
	FROM Library.CheckedOut C
	WHERE C.TransactionId = @TransactionId
	
GO