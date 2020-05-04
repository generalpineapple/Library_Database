CREATE OR ALTER PROCEDURE Library.CreateBook
   @ISBN NVARCHAR(16),
   @AuthorName NVARCHAR(256),
   @Title NVARCHAR(1024),
   @GenreName NVARCHAR(256),
   @ConditionType NVARCHAR(64),
   @BookId INT OUTPUT
AS
(SELECT A.AuthorId
FROM Library.Authors A 
WHERE A.AuthorName = @AuthorName) AS authorId

(SELECT G.GenreId
FROM Library.Genres G 
WHERE G.GenreName.LIKE '%@GenreName%') AS genreId

(SELECT C.ConditionId
FROM Library.Condition C 
WHERE C.ConditionType = @ConditionType)AS conditionId

INSERT Library.Books(ISBN, AuthorId, Title, GenreId, ConditionId)
VALUES(@ISBN, authorId, @Title, genreId, conditionId);

SET @BookId = SCOPE_IDENTITY();
GO