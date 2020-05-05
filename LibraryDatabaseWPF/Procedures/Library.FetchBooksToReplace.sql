/* QUERY 1
   List of books that need to be replaced, based on condition and popularity
 */
DROP PROCEDURE IF EXISTS [Library].FetchBooksToReplace
GO

CREATE PROCEDURE [Library].FetchBooksToReplace
AS
SELECT  B.BookId, B.ISBN, B.Title, B.AuthorId, B.GenreId, B.ConditionId
FROM Library.Books B
    INNER JOIN  
        Library.Condition C ON C.ConditionId = B.ConditionId
WHERE 
    C.ConditionType = 'Replace'
ORDER BY
    B.BookId ASC;
GO
	
