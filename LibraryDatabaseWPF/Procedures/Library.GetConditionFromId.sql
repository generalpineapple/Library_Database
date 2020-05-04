/*** Returns the condition type based on ID. Throws an error if the condition can't be found
****/
DROP PROCEDURE IF EXISTS [Library].GetConditionFromId
GO

CREATE PROCEDURE [Library].GetConditionFromId
@ConditionId INT
AS

BEGIN TRY
	SELECT C.ConditionType
	FROM Library.Condition C
	WHERE C.ConditionId = @ConditionId
	
	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No Condition with ID %d exists.', @ConditionId);
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when attempting to find the condition: ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO