DECLARE @CourierId INT, @CourierName NVARCHAR(100), @DeliveryCount INT;

-- Створюємо курсор для вибору всіх кур'єрів
DECLARE courierCursor CURSOR FOR
SELECT u.Id, u.FirstName
FROM Users u
WHERE u.Role = 2;

-- Відкриваємо курсор
OPEN courierCursor;

-- Читаємо перший рядок
FETCH NEXT FROM courierCursor INTO @CourierId, @CourierName;

-- Проходимо по всіх кур'єрах
WHILE @@FETCH_STATUS = 0
BEGIN
    -- Підраховуємо кількість доставок кур'єра
    SELECT @DeliveryCount = COUNT(*)
    FROM DeliveryInfo
    WHERE CourierId = @CourierId;

    -- Виводимо інформацію про кур'єра
    PRINT 'Courier: ' + @CourierName + ' | Deliveries: ' + CAST(@DeliveryCount AS NVARCHAR);

    -- Отримуємо наступного кур'єра
    FETCH NEXT FROM courierCursor INTO @CourierId, @CourierName;
END;

-- Закриваємо і видаляємо курсор
CLOSE courierCursor;
DEALLOCATE courierCursor;
