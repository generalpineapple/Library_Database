DROP PROCEDURE IF EXISTS Library.CreateAuthor;
DROP PROCEDURE IF EXISTS Library.CreateBook;
DROP PROCEDURE IF EXISTS Library.CreateCheckedOut;
DROP PROCEDURE IF EXISTS Library.CreateInventory;
DROP PROCEDURE IF EXISTS Library.CreateUserReport;
DROP PROCEDURE IF EXISTS Library.CreateUser;
DROP PROCEDURE IF EXISTS Library.EditBookQuality;
DROP PROCEDURE IF EXISTS Library.EditInventoryByISBN;
DROP PROCEDURE IF EXISTS Library.EditUserById;
DROP PROCEDURE IF EXISTS Library.FetchAllBooks;
DROP PROCEDURE IF EXISTS Library.FetchAllInventory;
DROP PROCEDURE IF EXISTS Library.FetchAllUsers;
DROP PROCEDURE IF EXISTS Library.FetchBookByAuthor;
DROP PROCEDURE IF EXISTS Library.FetchBookByISBN;
DROP PROCEDURE IF EXISTS [Library].[FetchBooksByISBN];
DROP PROCEDURE IF EXISTS Library.FetchBookByTitle;
DROP PROCEDURE IF EXISTS Library.FetchBooksToReplace;
DROP PROCEDURE IF EXISTS Library.FetchInventoryByISBN;
DROP PROCEDURE IF EXISTS Library.FetchTransactionById;
DROP PROCEDURE IF EXISTS [Library].FetchUserByName;
DROP PROCEDURE IF EXISTS Library.FetchUsersRentingBookByISBN;
DROP PROCEDURE IF EXISTS Library.GetAuthorIdFromName;
DROP PROCEDURE IF EXISTS Library.GetAuthorNameFromId;
DROP PROCEDURE IF EXISTS Library.GetBookFromId;
DROP PROCEDURE IF EXISTS Library.GetBooksFromUser;
DROP PROCEDURE IF EXISTS Library.GetConditionFromId;
DROP PROCEDURE IF EXISTS Library.GetGenreNameFromId;
DROP PROCEDURE IF EXISTS Library.GetTopBooksByGenre;
DROP PROCEDURE IF EXISTS Library.GetTopUsers;
DROP PROCEDURE IF EXISTS [Library].IncrementCheckouts;
DROP PROCEDURE IF EXISTS Library.ReturnCheckedOut;
GO


CREATE OR ALTER PROCEDURE Library.CreateAuthor
   @AuthorName NVARCHAR(256),
   @AuthorId INT OUTPUT
AS

INSERT Library.Authors(AuthorName)
VALUES(@AuthorName);

SET @AuthorId = SCOPE_IDENTITY();
GO

CREATE OR ALTER PROCEDURE Library.CreateBook
   @ISBN NVARCHAR(16),
   @AuthorId INT,
   @Title NVARCHAR(1024),
   @GenreId INT,
   @ConditionId INT,
   @BookId INT OUTPUT
AS

INSERT Library.Books(ISBN, AuthorId, Title, GenreId, ConditionId)
VALUES(@ISBN, @AuthorId, @Title, @GenreId, @ConditionId);

SET @BookId = SCOPE_IDENTITY();
GO

CREATE OR ALTER PROCEDURE Library.CreateCheckedOut
   @BookId INT,
   @UserId INT
AS

INSERT Library.CheckedOut(BookId, UserId)
VALUES(@BookId, @UserId);
GO

CREATE OR ALTER PROCEDURE Library.CreateInventory
   @ISBN NVARCHAR(16),
   @TotalCopies INT,
AS
INSERT Library.Inventory(ISBN, TotalCopies)
VALUES(@ISBN, @TotalCopies);
GO

CREATE PROCEDURE [Library].[CreateUserReport]
   @UserId VARCHAR(64)
AS
SELECT 
    users.[Name],
    users.UserId,
    users.TotalCheckouts, 
    (users.TotalCheckouts - (
        SELECT 
            SUM([out].UserId)
        FROM 
            Library.CheckedOut [out]
        WHERE
            [out].UserId = users.UserId
            AND 
            [out].ReturnedDate > [out].DueDate
    )) AS OnTimeReturns,
    users.LateReturns, 
    (
        SELECT 
            SUM([out].UserId)
        FROM 
            Library.CheckedOut [out]
        WHERE
            [out].UserId = users.UserId
            AND 
            [out].ReturnedDate > [out].DueDate
    ) AS OverDueBooks, 
    (15*(
        SELECT 
            SUM([out].UserId)
        FROM 
            Library.CheckedOut [out]
        WHERE
            [out].UserId = users.UserId
            AND 
            [out].ReturnedDate > [out].DueDate
    )) AS LateFees 
FROM 
    Library.Users users
WHERE 
    users.[UserId] = @UserId; 
GO

CREATE OR ALTER PROCEDURE Library.CreateUser
   @Name NVARCHAR(256),
   @Address NVARCHAR(256),
   @PhoneNumber NVARCHAR(256),
   @Email NVARCHAR(256)
AS
INSERT Library.Users(Name, Address, PhoneNumber, Email)
VALUES(@Name, @Address, @PhoneNumber, @Email);
GO
--SET @UserId = SCOPE_IDENTITY();

CREATE PROCEDURE [Library].EditBookQuality
   @BookId INT,
   @conditionId INT
AS
	UPDATE Library.Books
	SET ConditionId = @conditionId
	WHERE BookId = @BookId;
GO

CREATE PROCEDURE [Library].EditInventoryByISBN
	@ISBN NVARCHAR(16),
	@TotalCopies INT,
	@TotalCheckouts INT
AS
	UPDATE Library.Inventory
	SET TotalCopies = @TotalCopies,
		TotalCheckouts = @TotalCheckouts
	WHERE ISBN = @ISBN;
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

CREATE PROCEDURE [Library].FetchAllBooks
AS
SELECT B.BookId, B.ISBN, B.AuthorId, B.Title, B.GenreId, B.ConditionId
FROM Library.Books B;
GO

CREATE PROCEDURE [Library].FetchAllInventory
AS
SELECT I.ISBN, I.TotalCopies, I.TotalCheckouts
FROM Library.Inventory I
ORDER BY TotalCopies;
GO

CREATE PROCEDURE [Library].FetchAllUsers
AS
SELECT U.UserId, U.Name, U.TotalCheckouts, U.PhoneNumber, U.Email, U.LateReturns
FROM Library.Users U;
GO

CREATE PROCEDURE [Library].FetchUserByName
   @Name NVARCHAR(256)
AS
SELECT U.UserId, U.Name, U.TotalCheckouts, U.PhoneNumber, U.Email, U.LateReturns
FROM Library.Users U
WHERE U.Name Like @Name;
GO

CREATE PROCEDURE [Library].FetchBookByAuthor
   @Author NVARCHAR(256)
AS
    SELECT B.BookId, B.ISBN, B.Title, B.AuthorId, B.GenreId, B.ConditionId
    FROM [Library].Books B
    INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
    WHERE A.AuthorName = @Author
    ORDER BY B.Title ASC;
GO

CREATE PROCEDURE [Library].FetchBooksByISBN
   @ISBN INT
AS
    SELECT B.BookId, B.ISBN, B.Title, B.AuthorId, B.GenreId, B.ConditionId
    FROM [Library].Books B
    WHERE B.ISBN = @ISBN
    ORDER BY B.Title ASC;
GO

CREATE PROCEDURE [Library].FetchBookByTitle
   @Title VARCHAR(64)
AS
    SELECT B.BookId, B.ISBN, B.Title, B.AuthorId, B.GenreId, B.ConditionId
    FROM [Library].Books B
    WHERE B.Title LIKE @Title;
GO

CREATE PROCEDURE [Library].FetchBooksToReplace
AS
SELECT  B.BookId, B.ISBN, B.Title, B.AuthorId, B.GenreId, B.ConditionId
FROM Library.Books B
    INNER JOIN
        Library.Condition C ON C.ConditionId = B.ConditionId
WHERE
    C.ConditionType = 'Replace'
ORDER BY
    B.BookId ASC;
GO

CREATE PROCEDURE [Library].FetchInventoryByISBN
@ISBN NVARCHAR(16)
AS
	SELECT I.ISBN, I.TotalCopies, I.TotalCheckouts
	FROM Library.Inventory I
	WHERE I.ISBN = @ISBN
	ORDER BY TotalCopies;
GO

CREATE PROCEDURE [Library].FetchTransactionById
@TransactionId INT
AS
	SELECT C.BookId, C.UserId, C.CheckoutDate, C.ReturnedDate
	FROM Library.CheckedOut C
	WHERE C.TransactionId = @TransactionId;
GO

CREATE PROCEDURE [Library].FetchUsersRentingBookByISBN
   @ISBN INT
AS
    SELECT U.UserId, U.Name, U.TotalCheckouts, U.PhoneNumber, U.Email, U.LateReturns
    FROM [Library].CheckedOut Ch
    INNER JOIN [Library].Users U ON Ch.UserId = U.UserId
	    INNER JOIN [Library].Books B ON Ch.BookId = B.BookId
    WHERE B.ISBN = @ISBN
    ORDER BY U.Name ASC;
GO

CREATE PROCEDURE [Library].GetAuthorIdFromName
@AuthorName VARCHAR(256)
AS
	SELECT A.AuthorId
	FROM Library.Authors A
	WHERE A.AuthorName = @AuthorName;
GO

CREATE PROCEDURE [Library].GetAuthorNameFromId
@AuthorId INT
AS
	SELECT A.AuthorName
	FROM Library.Authors A
	WHERE A.AuthorId = @AuthorId;
GO

CREATE PROCEDURE [Library].GetBookFromId
@BookId INT
AS
	SELECT B.ISBN, B.AuthorId, B.Title, B.GenreId, B.ConditionId
	FROM Library.Books B
	WHERE B.BookId = @BookId;
GO

CREATE PROCEDURE [Library].GetBooksFromUser
   @Username VARCHAR(64)
AS
    SELECT B.BookId, B.ISBN, B.Title, A.AuthorName, Ch.CheckoutDate
    FROM [Library].CheckedOut Ch
    INNER JOIN [Library].Users U ON Ch.UserId = U.UserId
	    INNER JOIN [Library].Books B ON Ch.BookId = B.BookId
		    INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
    WHERE U.[Name] LIKE @Username
    ORDER BY B.Title ASC;
GO

CREATE PROCEDURE [Library].GetConditionFromId
@ConditionId INT
AS
	SELECT C.ConditionType
	FROM Library.Condition C
	WHERE C.ConditionId = @ConditionId;
GO

CREATE PROCEDURE [Library].GetGenreNameFromId
@GenreId INT
AS
	SELECT G.GenreName
	FROM Library.Genres G
	WHERE G.GenreId = @GenreId;
GO

CREATE PROCEDURE [Library].GetTopBooksByGenre
AS
    SELECT
        *
    FROM
        Library.Genres genres
    CROSS APPLY
    (
        SELECT TOP(5)
            books.BookId,
            books.ISBN,
            books.Title,
            books.GenreId,
            books.AuthorId,
            books.ConditionId
        FROM
            Library.Inventory inventory
            INNER JOIN
                Library.Books books ON books.ISBN = inventory.ISBN
        WHERE
            books.GenreId = genres.GenreId
        ORDER BY
            inventory.TotalCheckouts
    ) AS AllBooks;
GO

CREATE PROCEDURE [Library].GetTopUsers
AS
     SELECT TOP (10) U.UserId, U.Name, U.TotalCheckouts, U.PhoneNumber, U.Email, U.LateReturns
	FROM Library.Users u
	ORDER BY U.TotalCheckouts, U.UserId;
GO

CREATE PROCEDURE [Library].IncrementCheckouts
     @UserId INT
AS
     UPDATE [Library].Users
     SET TotalCheckouts = TotalCheckouts + 1
     WHERE UserId = @UserId;
GO

CREATE PROCEDURE [Library].ReturnCheckedOut
   @TransactionId INT,
   @BookId INT
AS
	UPDATE Library.CheckedOut
	SET ReturnedDate = GETDATE()
	WHERE TransactionId = @TransactionId OR BookId = @BookId;
GO

