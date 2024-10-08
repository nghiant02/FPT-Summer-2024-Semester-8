USE [eStoreDB]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 6/30/2024 10:00:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 6/30/2024 10:00:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[CompanyName] [nvarchar](100) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 6/30/2024 10:00:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[RequiredDate] [datetime] NOT NULL,
	[ShippedDate] [datetime] NULL,
	[Freight] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 6/30/2024 10:00:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Discount] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/30/2024 10:00:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Weight] [decimal](10, 2) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[UnitsInStock] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Electronics')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Clothing')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (3, N'Books')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (4, N'Home & Kitchen')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (1, N'john.doe@example.com', N'Doe Enterprises', N'New York', N'USA', N'password123')
INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (2, N'jane.smith@example.com', N'Smith Ltd.', N'London', N'UK', N'password456')
INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (3, N'alice.johnson@example.com', N'Johnson & Co.', N'Sydney', N'Australia', N'password789')
SET IDENTITY_INSERT [dbo].[Member] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (1, 1, CAST(N'2024-06-29T19:43:05.503' AS DateTime), CAST(N'2024-06-29T19:43:05.503' AS DateTime), CAST(N'2024-06-29T19:43:05.503' AS DateTime), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (2, 2, CAST(N'2023-06-02T00:00:00.000' AS DateTime), CAST(N'2023-06-06T00:00:00.000' AS DateTime), CAST(N'2023-06-04T00:00:00.000' AS DateTime), CAST(15.00 AS Decimal(18, 2)))
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (3, 3, CAST(N'2024-06-29T19:32:29.437' AS DateTime), CAST(N'2024-06-29T19:32:29.437' AS DateTime), CAST(N'2024-06-29T19:32:29.437' AS DateTime), CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (1, 1, CAST(699.99 AS Decimal(18, 2)), 1, 0.1)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (1, 3, CAST(19.99 AS Decimal(18, 2)), 2, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (2, 2, CAST(999.99 AS Decimal(18, 2)), 1, 0.05)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (2, 4, CAST(49.99 AS Decimal(18, 2)), 1, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (3, 3, CAST(0.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (3, 5, CAST(14.99 AS Decimal(18, 2)), 3, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (3, 7, CAST(10.00 AS Decimal(18, 2)), 10, 10)
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitsInStock]) VALUES (1, 1, N'Smartphone', CAST(0.50 AS Decimal(10, 2)), CAST(699.99 AS Decimal(18, 2)), 100)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitsInStock]) VALUES (2, 1, N'Laptop', CAST(2.50 AS Decimal(10, 2)), CAST(999.99 AS Decimal(18, 2)), 50)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitsInStock]) VALUES (3, 2, N'T-Shirt', CAST(0.20 AS Decimal(10, 2)), CAST(19.99 AS Decimal(18, 2)), 200)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitsInStock]) VALUES (4, 2, N'Jeans', CAST(0.80 AS Decimal(10, 2)), CAST(49.99 AS Decimal(18, 2)), 150)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitsInStock]) VALUES (5, 3, N'Novel', CAST(0.30 AS Decimal(10, 2)), CAST(14.99 AS Decimal(18, 2)), 300)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitsInStock]) VALUES (6, 3, N'Textbook', CAST(1.20 AS Decimal(10, 2)), CAST(89.99 AS Decimal(18, 2)), 100)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitsInStock]) VALUES (7, 2, N'string', CAST(10.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(18, 2)), 10)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitsInStock]) VALUES (10, 1, N'string', CAST(0.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(18, 2)), 0)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Product]  WITH NOCHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
