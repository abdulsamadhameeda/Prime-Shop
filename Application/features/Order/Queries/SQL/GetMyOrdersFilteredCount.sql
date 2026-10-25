SELECT COUNT(*)
FROM Orders o
WHERE o.UserId = @UserId
  AND (@Status IS NULL OR o.Status = @Status)
  AND (@FromDate IS NULL OR o.OrderDate >= CAST(@FromDate AS date))
  AND (@ToDate IS NULL OR o.OrderDate < DATEADD(DAY, 1, CAST(@ToDate AS date)));