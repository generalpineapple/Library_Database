/****
   Edit an inventory object of a given ISBN, will throw an error if no inventory exists with given ISBN
**/
DROP PROCEDURE IF EXISTS [Library].EditInventoryByISBN
GO

CREATE PROCEDURE [Library].EditInventoryByISBN
	@ISBN NVARCHAR(16),
	@TotalCopies INT,
	@TotalCheckouts INT 
AS


	UPDATE Library.Inventory
	SET TotalCopies = @TotalCopies,
		TotalCheckouts = @TotalCheckouts
	WHERE ISBN = @ISBN;
	
GO