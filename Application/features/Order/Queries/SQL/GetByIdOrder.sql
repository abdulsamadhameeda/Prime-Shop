SELECT 
    o.Id,
    o.OrderDate,
    o.TotalAmount,
    o.Status,
    o.ShippingStreet,
    o.ShippingCityId,
    l.Value AS ShippingCity, 
    o.UserId
FROM Orders o
LEFT JOIN Lookups l ON o.ShippingCityId = l.Id
WHERE o.Id = @Id