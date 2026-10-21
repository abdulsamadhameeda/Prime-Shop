SELECT 
    p.Id,
    p.Name,
    p.Description,
    p.Price,
    p.StockQuantity,
    p.ImageUrl,
    p.CategoryId,
    c.Name AS CategoryName
FROM Products p
INNER JOIN Categories c ON c.Id = p.CategoryId
WHERE p.Id = @id;