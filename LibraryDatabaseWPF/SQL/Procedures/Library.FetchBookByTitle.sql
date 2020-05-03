/**************************************
 * Procedure to return a Book using it's Title 
 **************************************/
DROP PROCEDURE IF EXISTS [Library].FetchBookByTitle
GO

CREATE PROCEDURE [Library].FetchBookByTitle
   @Title VARCHAR(64)
AS

SELECT B.BookId, B.ISBN, B.Title, A.AuthorName, B.GenreName, B.ConditionType 
FROM [Library].Books B
INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
WHERE B.Title LIKE '%@Title%';

GO