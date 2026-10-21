SELECT u.Id,
       u.FirstName,
       u.LastName,
       u.Email,
       u.PhoneNumber
FROM AspNetUsers u
WHERE u.Id = @Id
  AND u.IsDeleted = 0