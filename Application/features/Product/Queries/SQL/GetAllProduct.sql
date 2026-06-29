SELECT * FROM Products
WHERE (@CategoryId IS NULL OR CategoryId = @CategoryId)
AND (@MinPrice IS NULL OR Price >= @MinPrice)
AND (@MaxPrice IS NULL OR Price <= @MaxPrice)