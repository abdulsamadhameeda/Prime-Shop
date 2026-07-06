SELECT * FROM Orders
WHERE (Id = @OrderId  or @OrderId is null)
and (CustomerId = @CustomerId )