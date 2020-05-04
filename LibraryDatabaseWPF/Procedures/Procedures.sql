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
VALUES(@AuthorName, @TotalCopies);
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
FROM Library.Users U

CREATE PROCEDURE [Library].FetchBookByAuthor
   @Author NVARCHAR(256)
AS
    SELECT B.BookId, B.ISBN, B.Title, B.GenreId, B.ConditionId 
    FROM [Library].Books B
    INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
    WHERE A.AuthorName = @Author
    ORDER BY B.Title ASC;
GO

CREATE PROCEDURE [Library].FetchBooksByISBN
   @ISBN INT
AS
    SELECT B.BookId, B.AuthorId, B.Title, B.GenreId, B.ConditionId
    FROM [Library].Books B
    WHERE B.ISBN = @ISBN
    ORDER BY B.Title ASC;
GO

CREATE PROCEDURE [Library].FetchBookByTitle
   @Title VARCHAR(64)
AS
    SELECT B.BookId, B.ISBN, B.Title, B.AuthorId, B.GenreId, B.ConditionId
    FROM [Library].Books B
    WHERE B.Title LIKE '%@Title%';
GO

CREATE PROCEDURE [Library].FetchBooksToReplace
AS
SELECT
    B.BookId,
    B.ISBN,
    B.AuthorId,
    B.Title,
    B.GenreId,
    B.ConditionId
FROM
    (
        SELECT
            BookId,
            AuthorId,
            Title,
            GenreName
        FROM
            Library.Books
        WHERE
            ConditionType = 'Replace'
    ) B
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
    SELECT U.UserId, U.[Name], U.PhoneNumber
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
	WHERE A.AuthorName LIKE N'%@AuthorName%';
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
    WHERE U.[Name] LIKE N'%@Username%'
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
	SELECT TOP(10)
		u.Name,
		u.UserId,
		u.TotalCheckouts,
		u.LateReturns
	FROM Library.Users u
	ORDER BY u.TotalCheckouts;
GO
	
CREATE PROCEDURE [Library].ReturnCheckedOut
   @TransactionId INT,
   @BookId INT
AS
	UPDATE Library.CheckedOut
	SET ReturnedDate = GETDATE() 		
	WHERE TransactionId = @TransactionId OR BookId = @BookId;
GO
