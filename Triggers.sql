CREATE TRIGGER trg_SetMaxRatingOnCompletion
    ON DeliveryStatusHistory
    AFTER INSERT
    AS
BEGIN
    UPDATE Users
    SET Rating = 5
    FROM Users
    WHERE Id IN (
        SELECT d.CourierId
        FROM DeliveryInfo d
                 JOIN inserted i ON d.Id = i.DeliveryInfoId
        WHERE i.Status = 3
    );
END;

CREATE TRIGGER trg_PreventNegativePrice
ON Packages
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE Price < 0)
    BEGIN
        RAISERROR ('Price can not be less 0', 16, 1);
        ROLLBACK;
        RETURN;
    END;

    INSERT INTO Packages (DeliveryInfoId, Weight, Dimensions, Content, Fragile, Price)
    SELECT DeliveryInfoId, Weight, Dimensions, Content, Fragile, Price
    FROM inserted;
END;

-- Check 1
    
SELECT * FROM Users WHERE Role = 2; 
SELECT * FROM DeliveryInfo; 
SELECT * FROM DeliveryStatusHistory; 

INSERT INTO DeliveryStatusHistory (DeliveryInfoId, Status, ChangeDate) 
VALUES (4, 3, GETDATE()),
        (2, 4, GETDATE());

SELECT CourierId FROM DeliveryInfo WHERE Id = 4;
SELECT Id, FirstName, LastName, Rating FROM Users WHERE Id = 2;

-- Check 2
    
INSERT INTO Packages (DeliveryInfoId, Weight, Dimensions, Content, Fragile, Price)
VALUES
    (1, 2.5, '20/30', 'Books', 0, 5),
    (1, 2.5, '20/30', 'Books', 0, -10);
    
Select * from Packages;

INSERT INTO Packages (DeliveryInfoId, Weight, Dimensions, Content, Fragile, Price)
VALUES (1, 2.5, '20/30', 'Books', 0, 10);


