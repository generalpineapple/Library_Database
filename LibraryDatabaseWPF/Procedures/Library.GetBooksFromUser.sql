/**************************************
 * Procedure to list books checked out by a given user
 **************************************/
DROP PROCEDURE IF EXISTS [Library].GetBooksFromUser
GO

CREATE PROCEDURE [Library].GetBooksFromUser
   @Username VARCHAR(64)
AS
BEGIN TRY
    SELECT B.BookId, B.ISBN, B.Title, A.AuthorName, Ch.CheckoutDate
    FROM [Library].CheckedOut Ch
    INNER JOIN [Library].Users U ON Ch.UserId = U.UserId
	    INNER JOIN [Library].Books B ON Ch.BookId = B.BookId
		    INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
    WHERE U.[Name] LIKE N'%@Username%'
    ORDER BY B.Title ASC;

    IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No books from ISBN %s exists.', @Username);
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
