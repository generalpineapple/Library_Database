/****
   Returns all books
**/
DROP PROCEDURE IF EXISTS [Library].FetchAllBooks
GO

CREATE PROCEDURE [Library].FetchAllBooks
AS
SELECT B.BookId, B.ISBN, B.AuthorId, B.Title, B.GenreId, B.ConditionId
FROM Library.Books B
