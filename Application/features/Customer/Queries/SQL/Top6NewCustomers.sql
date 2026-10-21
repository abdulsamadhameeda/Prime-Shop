SELECT TOP 6
       u.Id,
       u.FirstName,
       u.LastName,
       u.Email,
       u.PhoneNumber
FROM AspNetUsers u
WHERE u.IsDeleted = 0
ORDER BY u.CreatedAt DESC;