/* Query 2 - Single User Report
 * Returns # of books checked out, # of books returned on time, # of books returned late,
 * Current # of overdue books, and the total cost of late fees. 
 **************************************/
 
DROP PROCEDURE IF EXISTS [Library].CreateUserReport
GO

CREATE PROCEDURE [Library].CreateUserReport
   @UserName VARCHAR(64)
AS

SELECT 
	U.TotalCheckouts,
	(
		SELECT COUNT(Ch.TransactionId)
		FROM [Library].CheckedOut Ch
		INNER JOIN [Library].Users U ON Ch.UserId = U.UserId
		WHERE U.Name = @UserName AND
			Ch.ReturnedDate IS NULL
	)AS CurrentCheckOuts,	
	(U.TotalCheckouts - U.LateReturns - 
		(
			SELECT COUNT(Ch.TransactionId)
			FROM [Library].CheckedOut Ch
			INNER JOIN [Library].Users U ON Ch.UserId = U.UserId
			WHERE U.Name = @UserName AND
				Ch.ReturnedDate IS NULL
		)
	)AS OnTimeReturns,
	U.LateReturns, 
	(
		SELECT COUNT(Ch.TransactionId)
		FROM [Library].CheckedOut Ch
		INNER JOIN [Library].Users U ON Ch.UserId = U.UserId
		WHERE U.Name = @UserName AND 
			Ch.ReturnedDate IS NULL AND 
			Ch.DueDate < GETDATE()
	)AS OverdueBooks,	
	SUM(Ch.ReturnedDate - Ch.DueDate) OVER (
	PARTITION BY Ch.UserId) AS DaysLate	
FROM [Library].Users U 
INNER JOIN [Library].CheckedOut Ch ON U.UserId = Ch.UserId
WHERE U.Name = @UserName
GROUP BY U.UserId, U.TotalCheckouts, U.LateReturns
ORDER BY U.TotalCheckouts DESC, U.LateReturns ASC;

GO
	