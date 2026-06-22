select * 
from Customers
where (@Name IS NULL 
   OR FirstName LIKE '%' + @Name + '%'
   OR LastName LIKE '%' + @Name + '%'
   OR (FirstName + ' ' + LastName) LIKE '%' + @Name + '%');