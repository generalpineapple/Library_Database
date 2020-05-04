/***Report query #4, returns a list of top 10 users by # of checkouts
****/
DROP PROCEDURE IF EXISTS [Library].GetTopUsers
GO

CREATE PROCEDURE [Library].GetTopUsers

AS

	SELECT TOP(10)
		u.Name,
		u.UserId,
		u.TotalCheckouts,
		u.LateReturns
	FROM Library.Users u
	ORDER BY u.TotalCheckouts;

	
GO
	
