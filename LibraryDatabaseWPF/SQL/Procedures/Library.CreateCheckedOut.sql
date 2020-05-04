CREATE OR ALTER PROCEDURE Library.CreateCheckedOut
   @BookId INT,
   @UserId INT,
   @TransactionId INT OUTPUT
AS

INSERT Library.CheckedOut(BookId, UserId)
VALUES(@BookId, @UserId);

SET @TransactionIdId = SCOPE_IDENTITY();
GO