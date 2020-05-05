/****
   Edit a user object of a given UserId, will throw an error if no user exists with given Id
**/
DROP PROCEDURE IF EXISTS [Library].EditUserById
GO

CREATE PROCEDURE [Library].EditUserById
   @Name NVARCHAR(256),
   @Address NVARCHAR(256),
   @PhoneNumber NVARCHAR(256),
   @Email NVARCHAR(256),
   @UserId INT 
AS


	UPDATE Library.Users
	SET Name = @Name, 
		Address = @Address, 
		PhoneNumber = @PhoneNumber, 
		Email = @Email
	WHERE UserId = @UserId;

	
GO
	
