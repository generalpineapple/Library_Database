/*** Returns the book associated with the given BookId
****/
DROP PROCEDURE IF EXISTS [Library].GetBookFromId
GO

CREATE PROCEDURE [Library].GetBookFromId
@BookId INT
AS


	SELECT B.ISBN, B.AuthorId, B.Title, B.GenreId, B.ConditionId
	FROM Library.Books B
	WHERE B.BookId = @BookId
	
GO