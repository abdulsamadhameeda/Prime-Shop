SELECT COUNT(*)
FROM Products p
WHERE (@CategoryId IS NULL OR p.CategoryId = @CategoryId)
  AND (@MinPrice IS NULL OR p.Price >= @MinPrice)
  AND (@MaxPrice IS NULL OR p.Price <= @MaxPrice);