SELECT 
    OI.Id,
    OI.OrderId,
    OI.ProductId,
    OI.Quantity,
    OI.UnitPrice,       
    p.Name AS ProductName
FROM OrderItems OI
INNER JOIN Products p ON OI.ProductId = p.Id
WHERE (@OrderId IS NULL OR OI.OrderId = @OrderId)