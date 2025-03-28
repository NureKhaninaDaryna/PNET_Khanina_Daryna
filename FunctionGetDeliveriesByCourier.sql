CREATE FUNCTION GetDeliveriesByCourier(@CourierId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT d.Id, d.DeliveryAddress, d.StartAddress, d.RecipientId, 
           (SELECT dbo.GetUserFullName(d.RecipientId)) AS RecipientName
    FROM DeliveryInfo d
    WHERE d.CourierId = @CourierId
);

SELECT * FROM dbo.GetDeliveriesByCourier(2);

