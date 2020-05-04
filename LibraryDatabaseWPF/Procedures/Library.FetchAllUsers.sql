/****
   Returns all users
**/
DROP PROCEDURE IF EXISTS [Library].FetchAllUsers
GO

CREATE PROCEDURE [Library].FetchAllUsers
AS
SELECT U.UserId, U.Name, U.TotalCheckouts, U.PhoneNumber, U.Email, U.LateReturns
FROM Library.Users U; 
GO
