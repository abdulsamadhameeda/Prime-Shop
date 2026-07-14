SELECT 
    MONTH(OrderDate) AS Month,
    COUNT(*) AS TotalOrders
FROM Orders
WHERE YEAR(OrderDate) = @year
GROUP BY MONTH(OrderDate)
ORDER BY Month