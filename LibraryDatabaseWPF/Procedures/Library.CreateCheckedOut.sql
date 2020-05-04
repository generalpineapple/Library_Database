CREATE OR ALTER PROCEDURE Library.CreateCheckedOut
   @BookId INT,
   @UserId INT
AS

INSERT Library.CheckedOut(BookId, UserId)
VALUES(@BookId, @UserId);

GO