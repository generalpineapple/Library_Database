/*** Returns the GenreId associated with the provied Genre name. Throws an error if the Genre can't be found
****/
DROP PROCEDURE IF EXISTS [Library].GetGenreNameFromId
GO

CREATE PROCEDURE [Library].GetGenreNameFromId
@GenreId INT
AS

BEGIN TRY
	SELECT G.GenreName
	FROM Library.Genres G
	WHERE G.GenreId = @GenreId
	
	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No Genre with Id %d exists.', @GenreId);
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when attempting to find the genre: ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO