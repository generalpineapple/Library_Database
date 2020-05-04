/*** Returns the AuthorId associated with the provied author name. Throws an error if the author can't be found
****/
DROP PROCEDURE IF EXISTS [Library].GetAuthorIdFromName
GO

CREATE PROCEDURE [Library].GetAuthorIdFromName
@AuthorName VARCHAR(256)
AS

BEGIN TRY
	SELECT A.AuthorId
	FROM Library.Authors A
	WHERE A.AuthorName LIKE N'%@AuthorName%'
	
	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No Author with name %s exists.', @AuthorName);
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