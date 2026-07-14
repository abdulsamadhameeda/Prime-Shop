SELECT ISNULL(SUM(TotalAmount), 0) 
FROM Orders 
WHERE Status = 4;