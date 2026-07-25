SELECT 
    o.Id,
    o.OrderDate,
    o.TotalAmount,
    o.Status,
    o.ShippingStreet,
    l.Value AS ShippingCity, 
    o.CustomerId
FROM Orders o
LEFT JOIN Lookups l ON o.ShippingCityId = l.Id
ORDER BY o.OrderDate DESC;