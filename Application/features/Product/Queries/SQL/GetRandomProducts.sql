SELECT TOP (@Count)
    p.Id,
    p.Name,
    p.Description,
    p.Price,
    p.StockQuantity,
    p.ProductImage,
    p.CategoryId,
    c.Name AS CategoryName
FROM Products p
INNER JOIN Categories c ON c.Id = p.CategoryId
ORDER BY NEWID();
