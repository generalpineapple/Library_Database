CREATE OR ALTER PROCEDURE Library.CreateBook
   @ISBN NVARCHAR(16),
   @AuthorId INT,
   @Title NVARCHAR(1024),
   @GenreId INT,
   @ConditionId INT,
   @BookId INT OUTPUT
AS

INSERT Library.Books(ISBN, AuthorId, Title, GenreId, ConditionId)
VALUES(@ISBN, @AuthorId, @Title, @GenreId, @ConditionId);

SET @BookId = SCOPE_IDENTITY();
GO