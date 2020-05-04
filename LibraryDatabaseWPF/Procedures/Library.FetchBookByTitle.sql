/**************************************
 * Procedure to return a Book using it's Title 
 **************************************/
DROP PROCEDURE IF EXISTS [Library].FetchBookByTitle
GO

CREATE PROCEDURE [Library].FetchBookByTitle
   @Title VARCHAR(64)
AS

    SELECT B.BookId, B.ISBN, B.Title, B.AuthorId, B.GenreId, B.ConditionId
    FROM [Library].Books B
    WHERE B.Title LIKE '%@Title%';

GO
