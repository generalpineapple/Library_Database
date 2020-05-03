CREATE OR ALTER PROCEDURE Library.CreateUser
   @Name NVARCHAR(256),
   @Address NVARCHAR(256),
   @PhoneNumber NVARCHAR(256),
   @Email NVARCHAR(256),
   @UserId INT OUTPUT
AS

INSERT Library.Users(Name, Address, PhoneNumber, Email)
VALUES(@Name, @Address, @PhoneNumber, @Email);

SET @UserId = SCOPE_IDENTITY();
GO