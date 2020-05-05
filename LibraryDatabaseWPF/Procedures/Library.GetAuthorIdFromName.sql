/*** Returns the AuthorId associated with the provied author name. Throws an error if the author can't be found
****/
DROP PROCEDURE IF EXISTS [Library].GetAuthorIdFromName
GO

CREATE PROCEDURE [Library].GetAuthorIdFromName
@AuthorName VARCHAR(256)
AS


	SELECT A.AuthorId
	FROM Library.Authors A
	WHERE A.AuthorName LIKE N'%@AuthorName%'
	
GO