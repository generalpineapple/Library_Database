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

BEGIN TRY
	UPDATE Library.Inventory
	SET TotalCopies = @TotalCopies
		TotalCheckouts = @TotalCheckouts
	WHERE ISBN = @ISBN;

	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No inventory with ISBN %s exists.', @ISBN);
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when updating the inventory: ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO