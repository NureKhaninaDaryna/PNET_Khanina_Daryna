CREATE PROCEDURE AssignCourierToDelivery
    @DeliveryInfoId INT
AS
BEGIN
    DECLARE @SelectedCourierId INT;
    DECLARE @DeliveryExists INT;

    SELECT @DeliveryExists = COUNT(*) FROM DeliveryInfo WHERE Id = @DeliveryInfoId;

    IF @DeliveryExists = 0
    BEGIN
        PRINT 'Delivery with provided ID - NOT FOUND';
        RETURN;
    END

    SELECT TOP 1 @SelectedCourierId = u.Id
    FROM Users u
    LEFT JOIN DeliveryInfo d ON u.Id = d.CourierId
    WHERE u.Role = 2
    GROUP BY u.Id
    ORDER BY COUNT(d.Id);

    IF @SelectedCourierId IS NULL
    BEGIN
        PRINT 'Free Courier - NOT FOUND';
        RETURN;
    END

    UPDATE DeliveryInfo
    SET CourierId = @SelectedCourierId
    WHERE Id = @DeliveryInfoId;

    PRINT 'Courier (ID: ' + CAST(@SelectedCourierId AS NVARCHAR) + ') assign on the delivery ' + CAST(@DeliveryInfoId AS NVARCHAR);
END;

EXEC AssignCourierToDelivery @DeliveryInfoId = 1;
    
EXEC AssignCourierToDelivery @DeliveryInfoId = 4444;

SELECT * FROM DeliveryInfo WHERE Id = 1;

-- DROP PROCEDURE AssignCourierToDelivery

