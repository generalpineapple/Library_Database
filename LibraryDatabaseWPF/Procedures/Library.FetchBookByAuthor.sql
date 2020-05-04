/**************************************
 * Procedure to fetch books by a given author
 **************************************/
DROP PROCEDURE IF EXISTS [Library].FetchBookByAuthor
GO

CREATE PROCEDURE [Library].FetchBookByAuthor
   @Author NVARCHAR(256)
AS

BEGIN TRY
    SELECT B.BookId, B.ISBN, B.Title, B.GenreName, B.ConditionType 
    FROM [Library].Books B
    INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
    WHERE A.AuthorName = @Author
    ORDER BY B.Title ASC;

    IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No books from author id %s exists.', @Author);
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