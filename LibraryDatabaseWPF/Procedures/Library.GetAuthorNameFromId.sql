/*** Returns the name associated with the provied AuthorId. Throws an error if the author can't be found
****/
DROP PROCEDURE IF EXISTS [Library].GetAuthorNameFromId
GO

CREATE PROCEDURE [Library].GetAuthorNameFromId
@AuthorId INT
AS

	SELECT A.AuthorName
	FROM Library.Authors A
	WHERE A.AuthorId = @AuthorId
	
GO