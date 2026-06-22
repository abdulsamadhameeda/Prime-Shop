SELECT *
FROM Products
WHERE (@Name IS NULL or  Name LIKE '%' + @Name + '%');