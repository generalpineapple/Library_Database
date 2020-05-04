/* QUERY 3
   List of genres ordered alphabetically with the top five books in each
   genre listed in descending popularity
 */
DROP PROCEDURE IF EXISTS [Library].GetTopBooksByGenre
GO

CREATE PROCEDURE [Library].GetTopBooksByGenre
   
AS
BEGIN TRY
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

    IF @@ROWCOUNT = 0
	BEGIN
		DECLARE @Message NVARCHAR(256) = FORMATMESSAGE(N'No books exists.');
		THROW 50000, @Message, 1;
	END;
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(1024) =
      N'An error occurred at line ' +
         CAST(ERROR_LINE() AS NVARCHAR(10)) +
         N' when attempting to find books ' +
         ERROR_MESSAGE();
         
   PRINT @ErrorMessage;

   THROW; -- Rethrow.
END CATCH;
GO
