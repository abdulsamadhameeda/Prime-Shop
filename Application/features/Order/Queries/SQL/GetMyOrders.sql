SELECT
    o.Id,
    o.OrderDate,
    o.TotalAmount,
    o.Status,
    o.ShippingStreet,
    l.Value AS ShippingCity,
    o.UserId
FROM Orders o
LEFT JOIN Lookups l ON l.Id = o.ShippingCityId
WHERE o.UserId = @UserId
  AND (@Status IS NULL OR o.Status = @Status)
  AND (@FromDate IS NULL OR o.OrderDate >= CAST(@FromDate AS date))
  AND (@ToDate IS NULL OR o.OrderDate < DATEADD(DAY, 1, CAST(@ToDate AS date)))
ORDER BY o.OrderDate DESC, o.Id DESC
OFFSET @Start ROWS FETCH NEXT @Length ROWS ONLY;