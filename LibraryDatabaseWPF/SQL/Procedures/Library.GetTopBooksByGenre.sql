/* QUERY 3
   List of genres ordered alphabetically with the top five books in each
   genre listed in descending popularity
 */
DROP PROCEDURE IF EXISTS [Library].GetTopBooksByGenre
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