/****
   A user returning a book, will throw an error if the book is not checked out
**/
DROP PROCEDURE IF EXISTS [Library].ReturnCheckedOut
GO

CREATE PROCEDURE [Library].ReturnCheckedOut
   @TransactionId INT,
   @BookId INT
AS

	UPDATE Library.CheckedOut
	SET ReturnedDate = GETDATE() 		
	WHERE TransactionId = @TransactionId OR BookId = @BookId;

	
GO