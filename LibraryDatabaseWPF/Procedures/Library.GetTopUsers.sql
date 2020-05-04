/***Report query #4, returns a list of top 10 users by # of checkouts
****/
DROP PROCEDURE IF EXISTS [Library].GetTopUsers
GO

CREATE PROCEDURE [Library].GetTopUsers

AS

BEGIN TRY
	SELECT TOP(10)
		u.Name,
		u.UserId,
		u.TotalCheckouts,
		u.LateReturns
	FROM Library.Users u
	OREDER BY u.TotalCheckouts

	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'Under 10 users');
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when retrieving top 10 users: ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO
	
