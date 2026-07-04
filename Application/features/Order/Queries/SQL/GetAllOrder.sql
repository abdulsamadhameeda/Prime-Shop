select * 
from Orders
where (Status = @Status or @Status IS NULL)
