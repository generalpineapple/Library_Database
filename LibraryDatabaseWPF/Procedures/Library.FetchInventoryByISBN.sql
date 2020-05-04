/*** Returns inventory associated with given ISBN
****/
DROP PROCEDURE IF EXISTS [Library].FetchInventoryByISBN
GO

CREATE PROCEDURE [Library].FetchInventoryByISBN
@ISBN NVARCHAR(16)
AS

	SELECT I.ISBN, I.TotalCopies, I.TotalCheckouts
	FROM Library.Inventory I
	WHERE I.ISBN = @ISBN
	ORDER BY TotalCopies;

	
GO