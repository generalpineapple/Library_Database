/**************************************
 * Procedure to return a Book using it's Title 
 **************************************/
DROP PROCEDURE IF EXISTS [Library].FetchBookByTitle
GO

CREATE PROCEDURE [Library].FetchBookByTitle
   @Title VARCHAR(64)
AS

BEGIN TRY
    SELECT B.BookId, B.ISBN, B.Title, A.AuthorName, B.GenreName, B.ConditionType 
    FROM [Library].Books B
    INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
    WHERE B.Title LIKE '%@Title%'

    IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No books from ISBN %s exists.', @Title);
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when attempting to find books ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO
