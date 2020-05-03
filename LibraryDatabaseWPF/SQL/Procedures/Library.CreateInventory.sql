CREATE OR ALTER PROCEDURE Library.CreateInventory
   @ISBN NVARCHAR(16),
   @TotalCopies INT,
AS

INSERT Library.Inventory(ISBN, TotalCopies)
VALUES(@AuthorName, @TotalCopies);

GO