/*** Returns the GenreId associated with the provied Genre name. Throws an error if the Genre can't be found
****/
DROP PROCEDURE IF EXISTS [Library].GetGenreNameFromId
GO

CREATE PROCEDURE [Library].GetGenreNameFromId
@GenreId INT
AS

	SELECT G.GenreName
	FROM Library.Genres G
	WHERE G.GenreId = @GenreId
	
GO