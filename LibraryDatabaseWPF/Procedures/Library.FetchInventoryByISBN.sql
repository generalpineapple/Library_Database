/*** Returns inventory associated with given ISBN
****/
DROP PROCEDURE IF EXISTS [Library].FetchInventoryByISBN
GO
@ISBN NVARCHAR(16)
CREATE PROCEDURE [Library].FetchInventoryByISBN
AS

BEGIN TRY
	SELECT I.ISBN, I.TotalCopies, I.TotalCheckouts
	FROM Library.Inventory I
	WHERE I.ISBN = @ISBN
	ORDER BY TotalCopies

	IF @@ROWCOUNT = 0
		BEGIN
			DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No Inventory with ISBN %s exists.', @ISBN);
			THROW 50000, @Message, 1;
		END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when attempting to find inventory: ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO