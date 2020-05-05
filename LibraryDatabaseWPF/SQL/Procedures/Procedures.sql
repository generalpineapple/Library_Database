DROP PROCEDURE IF EXISTS [Library].CreateAuthor;
GO

CREATE OR ALTER PROCEDURE Library.CreateAuthor
   @AuthorName NVARCHAR(256),
   @AuthorId INT OUTPUT
AS

INSERT Library.Authors(AuthorName)
VALUES(@AuthorName);

SET @AuthorId = SCOPE_IDENTITY();
GO

-- #############################################################################
DROP PROCEDURE IF EXISTS [Library].CreateBook;
GO

CREATE OR ALTER PROCEDURE Library.CreateBook
   @ISBN NVARCHAR(16),
   @AuthorName NVARCHAR(256),
   @Title NVARCHAR(1024),
   @GenreName NVARCHAR(256),
   @ConditionType NVARCHAR(64),
   @BookId INT OUTPUT
AS
(SELECT A.AuthorId
FROM Library.Authors A 
WHERE A.AuthorName = @AuthorName) AS authorId

(SELECT G.GenreId
FROM Library.Genres G 
WHERE G.GenreName.LIKE '%@GenreName%') AS genreId

(SELECT C.ConditionId
FROM Library.Condition C 
WHERE C.ConditionType = @ConditionType)AS conditionId

INSERT Library.Books(ISBN, AuthorId, Title, GenreId, ConditionId)
VALUES(@ISBN, authorId, @Title, genreId, conditionId);

SET @BookId = SCOPE_IDENTITY();
GO

-- #############################################################################
DROP PROCEDURE IF EXISTS [Library].CreateCheckedOut;
GO

CREATE OR ALTER PROCEDURE Library.CreateCheckedOut
   @BookId INT,
   @UserId INT,
   @TransactionId INT OUTPUT
AS

INSERT Library.CheckedOut(BookId, UserId)
VALUES(@BookId, @UserId);

SET @TransactionIdId = SCOPE_IDENTITY();
GOCREATE OR ALTER PROCEDURE Library.CreateInventory
   @ISBN NVARCHAR(16),
   @TotalCopies INT,
AS

INSERT Library.Inventory(ISBN, TotalCopies)
VALUES(@AuthorName, @TotalCopies);

GO

-- #############################################################################

DROP PROCEDURE IF EXISTS [Library].CreateUserReport;
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

-- #############################################################################
DROP PROCEDURE IF EXISTS [Library].CreateUser;
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

-- #############################################################################
DROP PROCEDURE IF EXISTS [Library].FetchBookByISBN;
GO

CREATE PROCEDURE [Library].FetchBooksByAuthor
   @Author NVARCHAR(256)
AS

SELECT B.BookId, B.ISBN, B.Title, B.GenreName, B.ConditionType 
FROM [Library].Books B
INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
WHERE A.AuthorName = @Author
ORDER BY B.Title ASC;
GO

-- #############################################################################
DROP PROCEDURE IF EXISTS [Library].FetchBookByTitle;
GO

CREATE PROCEDURE [Library].FetchBookByTitle
   @Title VARCHAR(64)
AS

SELECT B.BookId, B.ISBN, B.Title, A.AuthorName, B.GenreName, B.ConditionType 
FROM [Library].Books B
INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
WHERE B.Title LIKE '%@Title%';
GO

-- #############################################################################
DROP PROCEDURE IF EXISTS [Library].FetchBooksToReplace;
GO

CREATE PROCEDURE [Library].FetchBooksToReplace
   
AS

SELECT
    B.BookId,
    B.AuthorId,
    B.Title,
    B.GenreName,
    A.AuthorName
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
    INNER JOIN
        Library.Authors A ON A.AuthorId = B.AuthorId
ORDER BY
    B.BookId ASC;
	
GO
	
-- #############################################################################
DROP PROCEDURE IF EXISTS [Library].FetchUsersRentingBookByISBN;
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

-- #############################################################################
DROP PROCEDURE IF EXISTS [Library].GetBooksFromUser;
GO

CREATE PROCEDURE [Library].GetBooksFromUser
   @Username VARCHAR(64)
AS

SELECT B.BookId, B.ISBN, B.Title, A.AuthorName, Ch.CheckoutDate
FROM [Library].CheckedOut Ch
INNER JOIN [Library].Users U ON Ch.UserId = U.UserId
	INNER JOIN [Library].Books B ON Ch.BookId = B.BookId
		INNER JOIN [Library].Authors A ON B.AuthorId = A.AuthorId
WHERE U.[Name] = @Username
ORDER BY B.Title ASC;
GO

-- #############################################################################
DROP PROCEDURE IF EXISTS [Library].GetTopBooksByGenre;
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
        books.ISBN, 
        books.GenreId, 
        books.AuthorId, 
        inventory.TotalCheckouts
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
