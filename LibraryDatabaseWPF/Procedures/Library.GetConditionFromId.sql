/*** Returns the condition type based on ID. Throws an error if the condition can't be found
****/
DROP PROCEDURE IF EXISTS [Library].GetConditionFromId
GO

CREATE PROCEDURE [Library].GetConditionFromId
@ConditionId INT
AS


	SELECT C.ConditionType
	FROM Library.Condition C
	WHERE C.ConditionId = @ConditionId
	
GO