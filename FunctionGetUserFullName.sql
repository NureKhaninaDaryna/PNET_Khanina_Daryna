CREATE FUNCTION GetUserFullName(@UserId INT)
RETURNS NVARCHAR(255)
AS
BEGIN
    DECLARE @FullName NVARCHAR(255);

    SELECT @FullName = FirstName + ' ' + LastName
    FROM Users
    WHERE Id = @UserId;

    RETURN @FullName;
END;

SELECT dbo.GetUserFullName(1) AS 'Full name';

--DROP FUNCTION GetUserFullName
