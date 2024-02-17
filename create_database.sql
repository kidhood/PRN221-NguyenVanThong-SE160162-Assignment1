CREATE DATABASE InventoryManagement
GO
USE InventoryManagement
GO
CREATE TABLE account (
	id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(256),
	"user_name" VARCHAR(30),
	password VARCHAR (100),
	role INT
)

CREATE TABLE supplier (
	id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(256),
	phone VARCHAR(11),
	email VARCHAR(100),
	address NVARCHAR(256)
)

CREATE TABLE inventory (
	id INT PRIMARY KEY IDENTITY,
	inventory_type INT, 
	total_quantity INT DEFAULT 0,
	date DATETIME,
	total_cost DECIMAL(10, 2) NOT NULL,
	supplier_id INT,
	CONSTRAINT FK_supplier FOREIGN KEY (supplier_id) REFERENCES  supplier (id)
)

CREATE TABLE product (
	id INT PRIMARY KEY IDENTITY,
	name NVARCHAR(256),
	image_path VARCHAR(256),
	quantity INT DEFAULT 0,
	description NVARCHAR (256),
	price FLOAT,
	is_delete bit, 
	inventory_id INT
	CONSTRAINT FK_inventory FOREIGN KEY (inventory_id) REFERENCES  inventory (id)
)

CREATE TABLE "order" (
	id INT PRIMARY KEY IDENTITY,
	total_cost FLOAT DEFAULT 0,
	order_date DATETIME,
	account_id INT,
	CONSTRAINT FK_account FOREIGN KEY (account_id) REFERENCES  account (id)
)

CREATE TABLE order_detail (
	id INT PRIMARY KEY IDENTITY,
	quantiy INT DEFAULT 0,
	product_id INT,
	order_id INT
	CONSTRAINT FK_order FOREIGN KEY (order_id) REFERENCES  "order" (id),
	CONSTRAINT FK_product FOREIGN KEY (product_id) REFERENCES  product (id),
)

INSERT INTO account (name,user_name,password,[role]) VALUES
	 (N'Van Thong Staff',N'thongstaff',N'12345',1),
	 (N'Van Thong Manager',N'thongmanager',N'12345',2),
	 (N'Van Thong Admin',N'thongadmin',N'12345',3);

	INSERT INTO supplier (name,phone,email,address) VALUES
	 (N'Coca company',N'0303030303',N'coca@gmail.com',N'1751 Palmer Road');

	INSERT INTO inventory (id, inventory_type,total_quantity,[date],total_cost,supplier_id) VALUES
	 (7, 1,1450,'2024-02-16 00:00:00.0',1435.50,1);

	
INSERT INTO product (name, image_path, quantity, description, price, is_delete, inventory_id)
	VALUES ('Coca', '/images/coca.jpg', 150, 'Refreshing Coca Cola', 1.99, 0, 7),
       ('Snack', '/images/snack.jpg', 200, 'Delicious Snack', 2.49, 0, 7),
       ('Chips', '/images/chips.jpg', 100, 'Crunchy Potato Chips', 3.99, 0, 7),
       ('Soda', '/images/soda.jpg', 120, 'Fizzy Soda', 0.99, 0, 7),
       ('Chocolate', '/images/chocolate.jpg', 80, 'Smooth Milk Chocolate', 4.99, 0, 7),
       ('Cookies', '/images/cookies.jpg', 150, 'Homemade Cookies', 2.99, 0, 7),
       ('Juice', '/images/juice.jpg', 180, 'Fresh Fruit Juice', 1.49, 0, 7),
       ('Water', '/images/water.jpg', 250, 'Bottled Water', 0.75, 0, 7),
       ('Popcorn', '/images/popcorn.jpg', 120, 'Buttered Popcorn', 2.29, 0, 7),
       ('Energy Drink', '/images/energy.jpg', 100, 'High-Energy Drink', 3.49, 0, 7);
