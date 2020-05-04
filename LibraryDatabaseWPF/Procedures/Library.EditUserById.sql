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

BEGIN TRY
	UPDATE Library.Users
	SET Name = @Name, 
		Address = @Address, 
		PhoneNumber = @PhoneNumber, 
		Email = @Email
	WHERE UserId = @UserId;

	IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No user with UserId %d exists.', @UserId);
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when updating the user: ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO
	
