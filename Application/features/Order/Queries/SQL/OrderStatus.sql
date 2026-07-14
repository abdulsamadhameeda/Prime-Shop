SELECT 
    Status,
    COUNT(*) AS Total
FROM Orders
GROUP BY Status