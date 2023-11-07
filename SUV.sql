USE MASTER
GO
IF EXISTS (SELECT * FROM SYS.DATABASES WHERE Name = 'SONUNGVIEN')
BEGIN
	DROP DATABASE SONUNGVIEN
END
GO
CREATE DATABASE SONUNGVIEN
GO
USE SONUNGVIEN
GO
CREATE TABLE Category
( 
	CategoryID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CategoryName NVARCHAR(100) NOT NULL,

	[Order] INT,
	Meta VARCHAR(100),
	[Image] NVARCHAR(100),
	Hide BIT,
	CreatedAt DATETIME NOT NULL,
	UpdatedAt DATETIME NOT NULL,
);
CREATE TABLE Product
(
	ProductID INT NOT NULL IDENTITY (1,1),
	ProductName NVARCHAR(100) NOT NULL,
	ProductDescription NVARCHAR(MAX),
	ProductSold INT NOT NULL,
	ProductPrice Decimal NOT NULL,

	[Image] NVARCHAR(100),
	[Order] INT,
	Meta VARCHAR(100),
	Hide BIT,
	CreatedAt DATETIME NOT NULL,
	UpdatedAt DATETIME NOT NULL,

	CategoryID INT NOT NULL,
	PRIMARY KEY (ProductID),
	FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);
CREATE TABLE Discount
(
	DiscountID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	DiscountName NVARCHAR(100) NOT NULL,
	DiscountValue DECIMAL NOT NULL,
	DiscountCode VARCHAR(20) NOT NULL,
	DiscountCondition INT NOT NULL,
	DiscountMaxValue INT NOT NULL,
	DiscountRemark NVARCHAR(100) NOT NULL,
	DiscountAvailableFrom DATETIME NOT NULL,
	DiscountAvailableUntil DATETIME NOT NULL,

	[Order] INT,
	Meta VARCHAR(100),
	[Image] NVARCHAR(100),
	Hide BIT,
	CreatedAt DATETIME NOT NULL,
	UpdatedAt DATETIME NOT NULL,
);

CREATE TABLE DetailedProduct
(
	DetailedProductID INT NOT NULL IDENTITY (1,1),
	DetailedProductPrice DECIMAL NOT NULL,
	DetailedProductQuantity INT NOT NULL,
	DetailedProductName CHAR(100) NOT NULL,
	ProductID INT NOT NULL,

	[Order] INT,
	Meta VARCHAR(100),
	[Image] NVARCHAR(100),
	Hide BIT,
	CreatedAt DATETIME NOT NULL,
	UpdatedAt DATETIME NOT NULL,

	PRIMARY KEY (DetailedProductID),
	FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);
CREATE TABLE TermOfPayment
(
	PaymentID INT NOT NULL IDENTITY (1,1),
	PaymentName NVARCHAR(100) NOT NULL,

	Meta VARCHAR(100),
	[Image] NVARCHAR(100),
	Hide BIT,
	CreatedAt DATETIME NOT NULL,
	UpdatedAt DATETIME NOT NULL,

	PRIMARY KEY (PaymentID)
);

CREATE TABLE Bill
(
	BillID INT NOT NULL IDENTITY (1,1),
	BillPhoneNumber NVARCHAR(100) NOT NULL,
	BillLastName NVARCHAR(100) NOT NULL,
	BillFirstName NVARCHAR(100) NOT NULL,
	BillNote NVARCHAR(MAX) NOT NULL,
	BillPostcode NVARCHAR(100) NOT NULL,
	BillEmail NVARCHAR(100) NOT NULL,
	BillProvince NVARCHAR(100) NOT NULL,
	BillWard NVARCHAR(100) NOT NULL,
	BillAddress NVARCHAR(MAX) NOT NULL,
	BillUpdatedAt DATETIME NOT NULL,
	BillCreatedAt DATETIME NOT NULL,
	BillOldPrice DECIMAL NOT NULL,
	BillTaxAmount DECIMAL NOT NULL,
	BillPrice DECIMAL NOT NULL,
	BillDiscountAmount DECIMAL NOT NULL,
	BillFinalPrice DECIMAL NOT NULL,
	BillStt NVARCHAR(200) NOT NULL,

	Meta VARCHAR(100),
	Hide BIT,
	CreatedAt DATETIME NOT NULL,
	UpdatedAt DATETIME NOT NULL,

	BillTermOfPayment INT NOT NULL,
	PRIMARY KEY (BillID),
	FOREIGN KEY (BillTermOfPayment) REFERENCES TermOfPayment(PaymentID)
);

CREATE TABLE BillProduct
(
	BillID INT NOT NULL,
	DetailedProductID INT NOT NULL,
	Amount INT NOT NULL,

	[Order] INT,
	Meta VARCHAR(100),
	Hide BIT,
	CreatedAt DATETIME NOT NULL,
	UpdatedAt DATETIME NOT NULL,

	PRIMARY KEY (BillID, DetailedProductID),
	FOREIGN KEY (BillID) REFERENCES Bill(BillID),
	FOREIGN KEY (DetailedProductID) REFERENCES DetailedProduct(DetailedProductID)
);

CREATE TABLE DiscountUse
(
	DiscountID INT NOT NULL,
	BillID INT NOT NULL,
	UsedAt DATETIME NOT NULL,

	[Order] INT,
	Meta VARCHAR(100),
	Hide BIT,
	CreatedAt DATETIME NOT NULL,
	UpdatedAt DATETIME NOT NULL,

	PRIMARY KEY (DiscountID, BillID),
	FOREIGN KEY (DiscountID) REFERENCES Discount(DiscountID),
	FOREIGN KEY (BillID) REFERENCES Bill(BillID)
);
CREATE TABLE ProductImage
(
	ProductImageID INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	ProductImageLink VARCHAR(100),
	ProductID INT NOT NULL,
	FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
)