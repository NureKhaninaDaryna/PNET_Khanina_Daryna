use DeliveryDb;

db.createCollection("Users", {
    validator: {
        $jsonSchema: {
            bsonType: "object",
            required: ["Email", "PasswordHash", "Role", "UserName"],
            properties: {
                Email: { bsonType: "string" },
                PasswordHash: { bsonType: "string" },
                Role: { bsonType: "string", enum: ["user", "courier", "sender"] },
                AvatarId: { bsonType: "objectId" },
                DayOfBirth: { bsonType: "date" },
                FirstName: { bsonType: "string" },
                LastName: { bsonType: "string" },
                PhoneNumber: { bsonType: "string" },
                Rating: { bsonType: "double" },
                UserName: { bsonType: "string" }
            }
        }
    }
});

db.createCollection("ProfileImages", {
    validator: {
        $jsonSchema: {
            bsonType: "object",
            required: ["FileName", "ImageData"],
            properties: {
                FileName: { bsonType: "string" },
                ImageData: { bsonType: "binData" }
            }
        }
    }
});

db.createCollection("DeliveryInfo", {
    validator: {
        $jsonSchema: {
            bsonType: "object",
            required: ["CourierId", "SenderId", "RecipientId", "Status"],
            properties: {
                CourierId: { bsonType: "objectId" },
                SenderId: { bsonType: "objectId" },
                RecipientId: { bsonType: "objectId" },
                Status: { bsonType: "string", enum: ["pending", "in_progress", "delivered", "canceled"] },
                DateStatus: { bsonType: "date" },
                DateStatusChange: { bsonType: "date" },
                AddressInProgress: { bsonType: "string" },
                DeliveryAddress: { bsonType: "string" },
                StartAddress: { bsonType: "string" }
            }
        }
    }
});

db.createCollection("Packages", {
    validator: {
        $jsonSchema: {
            bsonType: "object",
            required: ["DeliveryInfoId", "Weight", "Dimensions", "Content", "Fragile", "Price"],
            properties: {
                DeliveryInfoId: { bsonType: "objectId" },
                Weight: { bsonType: "double" },
                Dimensions: { bsonType: "string" },
                Content: { bsonType: "string" },
                Fragile: { bsonType: "bool" },
                Price: { bsonType: "double" }
            }
        }
    }
});
