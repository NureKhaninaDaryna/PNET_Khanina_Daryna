-- Додавання тестових зображень профілю
INSERT INTO ProfileImages (FileName, ImageData) VALUES 
('avatar1.jpg', 0x),
('avatar2.jpg', 0x),
('avatar3.jpg', 0x),
('avatar4.jpg', 0x),
('avatar5.jpg', 0x);

-- Додавання тестових користувачів
INSERT INTO Users (Email, PasswordHash, Role, AvatarId, DayOfBirth, FirstName, LastName, PhoneNumber, Rating, UserName) VALUES 
('john.doe@email.com', 'hashed_password_1', 1, 1, '1990-05-10', 'John', 'Doe', '+123456789', 4.5, 'john_doe'),
('jane.smith@email.com', 'hashed_password_2', 2, 2, '1985-09-20', 'Jane', 'Smith', '+987654321', 4.7, 'jane_smith'),
('michael.brown@email.com', 'hashed_password_3', 2, 3, '1992-03-15', 'Michael', 'Brown', '+1122334455', 4.2, 'michael_b'),
('susan.white@email.com', 'hashed_password_4', 1, NULL, '1988-07-25', 'Susan', 'White', '+5544332211', 4.8, 'susan_w'),
('alex.jones@email.com', 'hashed_password_5', 1, 4, '1995-12-30', 'Alex', 'Jones', '+6677889900', 3.9, 'alex_j');

-- Додавання тестових доставок
INSERT INTO DeliveryInfo (CourierId, RecipientId, DeliveryAddress, StartAddress) VALUES 
(2, 1, '456 Elm Street, NY', '123 Main Street, NY'),
(3, 2, '789 Oak Avenue, CA', '567 Maple Road, CA'),
(2, 3, '321 Pine Lane, TX', '654 Cedar Blvd, TX'),
(4, 5, '987 Birch Way, FL', '345 Willow Drive, FL'),
(3, 4, '213 Spruce Ave, WA', '876 Redwood St, WA');

-- Додавання статусів доставки
INSERT INTO DeliveryStatusHistory (DeliveryInfoId, Status, ChangeDate, AddressInProgress) VALUES 
(1, 1, '2024-03-20 08:00:00', 'Address 1'),
(1, 2, '2024-03-21 09:15:00', 'Address 2'),
(1, 3, '2024-03-22 10:30:00', 'Address 3'),
(4, 1, '2024-03-23 11:45:00', 'Address 1'),
(5, 1, '2024-03-24 12:00:00', 'Address 1');

-- Додавання тестових посилок
INSERT INTO Packages (DeliveryInfoId, Weight, Dimensions, Content, Fragile, Price) VALUES 
(1, 2.5, '30x20x15', 'Electronics', 1, 49.99),
(1, 1.2, '25x15x10', 'Books', 0, 19.99),
(1, 3.0, '40x30x20', 'Clothes', 0, 29.99),
(4, 0.8, '20x10x5', 'Accessories', 1, 9.99),
(5, 5.0, '50x40x30', 'Home Appliances', 1, 99.99);
