# CSharpArcitecture2018

This Repo is used in the videotutorial about building a Clean Arcitecture Application in .net core:
https://www.youtube.com/watch?v=dAIpnZ-CsTs&list=PL8jcXf-CLpxqR7d2v7rVyEJn2H7xze_Oz

Have fun :)

# To initiate the DB run the following

### Drop All (Remove tables if they exists:

DROP TABLE IF EXISTS dbo.OrderLines, dbo.Orders, dbo.Products, dbo.Customers, dbo.CustomerTypes, dbo.AspNetUserClaims,
dbo.AspNetUserLogins, dbo.AspNetUserRoles, dbo.AspNetUserTokens, dbo.AspNetUsers, dbo.AspNetRoleClaims,dbo.AspNetRoles, dbo.users, dbo.roles


### Create All Required tables:

CREATE TABLE "CustomerTypes" ( "Id" INTEGER NOT NULL CONSTRAINT "PK_CustomerTypes" PRIMARY KEY IDENTITY(1,1), "Name" nvarchar(max) NULL );

CREATE TABLE "Products" ( "Id" INTEGER NOT NULL CONSTRAINT "PK_Products" PRIMARY KEY IDENTITY(1,1) , "Name" nvarchar(max) NULL, "Price" float NOT NULL );

CREATE TABLE "Roles" ( "Id" INTEGER NOT NULL CONSTRAINT "PK_Roles" PRIMARY KEY IDENTITY(1,1) , "Name" nvarchar(max) NULL );

CREATE TABLE "Customers" ( "Id" INTEGER NOT NULL CONSTRAINT "PK_Customers" PRIMARY KEY IDENTITY(1,1) , "FirstName" nvarchar(max) NULL, "LastName" nvarchar(max) NULL, "Address" nvarchar(max) NULL, "TypeId" INTEGER NULL, CONSTRAINT "FK_Customers_CustomerTypes_TypeId" FOREIGN KEY ("TypeId") REFERENCES "CustomerTypes" ("Id") ON DELETE SET NULL );

CREATE TABLE "Users" ( "Id" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY IDENTITY(1,1), "UserName" nvarchar(max) NULL, "Email" nvarchar(max) NULL, "PasswordHash" nvarchar(max) NULL, "RoleId" INTEGER NULL, CONSTRAINT "FK_Users_Roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE SET NULL );

CREATE TABLE "Orders" ( "Id" INTEGER NOT NULL CONSTRAINT "PK_Orders" PRIMARY KEY IDENTITY(1,1) , "OrderDate" datetime2 NOT NULL, "DeliveryDate" datetime2 NOT NULL, "CustomerId" INTEGER NULL, CONSTRAINT "FK_Orders_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id") ON DELETE SET NULL );

CREATE TABLE "OrderLines" ( "ProductId" INTEGER NOT NULL, "OrderId" INTEGER NOT NULL, "Qty" INTEGER NOT NULL, "PriceWhenBought" float NOT NULL, CONSTRAINT "PK_OrderLines" PRIMARY KEY ("ProductId", "OrderId"), CONSTRAINT "FK_OrderLines_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE, CONSTRAINT "FK_OrderLines_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE );

### Insert Some default Data:

INSERT INTO dbo.CustomerTypes ( name ) VALUES 
('Guest'), 
('VIP'), 
('Rich'),
('Soo Poor');

INSERT INTO dbo.Customers ( firstName, lastName, address, typeId) VALUES
('Bill1', 'Billson1', 'StreetRoad 122', 1), 
('Bill2', 'Billson2', 'StreetRoad 222', 2),
('Bill3', 'Billson3', 'StreetRoad 322', 3), 
('Bill4', 'Billson4', 'StreetRoad 422', 1), 
('Bill5', 'Billson5', 'StreetRoad 522', 1), 
('Bill6', 'Billson6', 'StreetRoad 622', 2), 
('Bill7', 'Billson7', 'StreetRoad 722', 1), 
('Bill8', 'Billson8', 'StreetRoad 822', 1), 
('Bill9', 'Billson9', 'StreetRoad 922', 3), 
('Bill10', 'Billson10', 'StreetRoad 1022', 4), 
('Bill11', 'Billson11', 'StreetRoad 1122', 4), 
('Bill12', 'Billson12', 'StreetRoad 1222', 3);

INSERT INTO dbo.Products ( name, price) VALUES 
('Frog', 22.22),
('Smurf Tattoos', 11.22),
('Cheese Carver', 131.22),
('Rocket', 12.22);

INSERT INTO dbo.Orders ( orderDate, deliveryDate, customerId) VALUES 
('2018-10-01', '2018-11-01', 1),
('2017-12-11', '2018-01-11', 2);
        
INSERT INTO dbo.OrderLines ( productId, orderId, qty, priceWhenBought) VALUES 
(1, 1, 2, 22.1),
(3, 1, 13, 131.22),
(2, 2, 2, 11.22),
(3, 2, 4, 131.22),
(4, 2, 1, 33.22);
        
INSERT INTO dbo.Roles (Name) VALUES 
('Guest'), 
('User'), 
('Administrator'), 
('SuperAdministrator')

INSERT INTO dbo.Users ( UserName, Email, PasswordHash, RoleId)
VALUES ('blinko', 'blinko@inko.dk', 'AQAAAAEAACcQAAAAEFE8XWu6lIyinwsA4bBYJiOvabmOqZoURROPGY/eJdiNES+RGLLU7VW+/g3I+aFepA==', 1), 
('dinko', 'dinko@inko.dk', 'AQAAAAEAACcQAAAAENLKdwf9yrsIwY92GvwzYNVkXgdjoqWkgtt2TNlExnM+8lHORdurnPFszwiVYvJrwQ==', 3) 

# After Init you can login as
### User with:
Username: blinko
PW: asdQWE123€

### Administrator with:
Username: dinko
PW: asdQWE123€
