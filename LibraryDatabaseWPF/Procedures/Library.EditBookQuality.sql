/****
   Updating the quality of a book by BookId, will throw an error if the book doesn't exist
**/
DROP PROCEDURE IF EXISTS [Library].EditBookQuality
GO

CREATE PROCEDURE [Library].EditBookQuality
   @BookId INT,
   @conditionId INT
AS
	
	UPDATE Library.Books
	SET ConditionId = @conditionId 		
	WHERE BookId = @BookId;

	GO