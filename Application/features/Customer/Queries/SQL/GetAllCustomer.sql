SELECT u.Id,
       u.FirstName,
       u.LastName,
       u.Email,
       u.PhoneNumber
FROM AspNetUsers u
WHERE u.IsDeleted = 0
  AND (@Name IS NULL
       OR u.FirstName LIKE '%' + @Name + '%'
       OR u.LastName LIKE '%' + @Name + '%'
       OR (u.FirstName + ' ' + u.LastName) LIKE '%' + @Name + '%');