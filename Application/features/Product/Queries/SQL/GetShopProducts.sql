SELECT
    p.Id,
    p.Name,
    p.Description,
    p.Price,
    p.StockQuantity,
    p.ImageUrl,
    p.CategoryId,
    c.Name AS CategoryName
FROM Products p
LEFT JOIN Categories c ON c.Id = p.CategoryId
WHERE (@CategoryId IS NULL OR p.CategoryId = @CategoryId)
  AND (@MinPrice IS NULL OR p.Price >= @MinPrice)
  AND (@MaxPrice IS NULL OR p.Price <= @MaxPrice)
ORDER BY
    CASE WHEN @SortBy = 'price-asc' THEN p.Price END ASC,
    CASE WHEN @SortBy = 'price-desc' THEN p.Price END DESC,
    CASE WHEN @SortBy = 'name-asc' THEN p.Name END ASC,
    CASE WHEN @SortBy = 'name-desc' THEN p.Name END DESC,
    p.Name ASC
OFFSET @Start ROWS
FETCH NEXT @Length ROWS ONLY;