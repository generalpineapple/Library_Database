/*** Returns all Inventory elements
****/
DROP PROCEDURE IF EXISTS [Library].FetchAllInventory
GO

CREATE PROCEDURE [Library].FetchAllInventory
AS
SELECT I.ISBN, I.TotalCopies, I.TotalCheckouts
FROM Library.Inventory I
ORDER BY TotalCopies