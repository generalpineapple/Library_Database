/***Report query #4, returns a list of top 10 users by # of checkouts
****/
DROP PROCEDURE IF EXISTS [Library].GetAuthorNameFromId
GO

CREATE PROCEDURE [Library].GetAuthorNameFromId
@AuthorId INT
AS

BEGIN TRY
	SELECT A.AuthorName
	FROM Library.Authors
	WHERE A.AuthorId = @AuthorId
	
	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No Author with Id %d exists.', @AuthorId);
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when attempting to find the author: ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO