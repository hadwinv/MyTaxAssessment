USE [TaxAdministration]
GO
ALTER TABLE [dbo].[TaxAssessment] DROP CONSTRAINT [FK_TaxAssessment_TaxType]
GO
ALTER TABLE [dbo].[TaxType] DROP CONSTRAINT [DF__TaxType__DateCre__37A5467C]
GO
ALTER TABLE [dbo].[TaxType] DROP CONSTRAINT [DF__TaxType__UserCre__36B12243]
GO
ALTER TABLE [dbo].[ProgressiveTaxRate] DROP CONSTRAINT [DF__Progressi__DateC__66603565]
GO
ALTER TABLE [dbo].[ProgressiveTaxRate] DROP CONSTRAINT [DF__Progressi__UserC__656C112C]
GO
ALTER TABLE [dbo].[FlatValueTaxRate] DROP CONSTRAINT [DF__FlatValue__DateC__4BAC3F29]
GO
ALTER TABLE [dbo].[FlatValueTaxRate] DROP CONSTRAINT [DF__FlatValue__UserC__4AB81AF0]
GO
ALTER TABLE [dbo].[FlatTaxRate] DROP CONSTRAINT [DF__FlatTaxRa__DateC__628FA481]
GO
ALTER TABLE [dbo].[FlatTaxRate] DROP CONSTRAINT [DF__FlatTaxRa__UserC__619B8048]
GO
/****** Object:  Table [dbo].[TaxType]    Script Date: 2020/07/15 02:10:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaxType]') AND type in (N'U'))
DROP TABLE [dbo].[TaxType]
GO
/****** Object:  Table [dbo].[TaxAssessment]    Script Date: 2020/07/15 02:10:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaxAssessment]') AND type in (N'U'))
DROP TABLE [dbo].[TaxAssessment]
GO
/****** Object:  Table [dbo].[ProgressiveTaxRate]    Script Date: 2020/07/15 02:10:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProgressiveTaxRate]') AND type in (N'U'))
DROP TABLE [dbo].[ProgressiveTaxRate]
GO
/****** Object:  Table [dbo].[FlatValueTaxRate]    Script Date: 2020/07/15 02:10:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FlatValueTaxRate]') AND type in (N'U'))
DROP TABLE [dbo].[FlatValueTaxRate]
GO
/****** Object:  Table [dbo].[FlatTaxRate]    Script Date: 2020/07/15 02:10:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FlatTaxRate]') AND type in (N'U'))
DROP TABLE [dbo].[FlatTaxRate]
GO
USE [master]
GO
/****** Object:  Database [TaxAdministration]    Script Date: 2020/07/15 02:10:17 PM ******/
DROP DATABASE [TaxAdministration]
GO
/****** Object:  Database [TaxAdministration]    Script Date: 2020/07/15 02:10:17 PM ******/
CREATE DATABASE [TaxAdministration]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaxAdministration', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TaxAdministration.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaxAdministration_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TaxAdministration.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TaxAdministration] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaxAdministration].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TaxAdministration] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [TaxAdministration] SET ANSI_NULLS ON 
GO
ALTER DATABASE [TaxAdministration] SET ANSI_PADDING ON 
GO
ALTER DATABASE [TaxAdministration] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [TaxAdministration] SET ARITHABORT ON 
GO
ALTER DATABASE [TaxAdministration] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TaxAdministration] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TaxAdministration] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TaxAdministration] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TaxAdministration] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [TaxAdministration] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [TaxAdministration] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TaxAdministration] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [TaxAdministration] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TaxAdministration] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TaxAdministration] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TaxAdministration] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TaxAdministration] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TaxAdministration] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TaxAdministration] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TaxAdministration] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TaxAdministration] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TaxAdministration] SET RECOVERY FULL 
GO
ALTER DATABASE [TaxAdministration] SET  MULTI_USER 
GO
ALTER DATABASE [TaxAdministration] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TaxAdministration] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TaxAdministration] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TaxAdministration] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TaxAdministration] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TaxAdministration] SET QUERY_STORE = OFF
GO
USE [TaxAdministration]
GO
/****** Object:  Table [dbo].[FlatTaxRate]    Script Date: 2020/07/15 02:10:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlatTaxRate](
	[FlatTaxRateId] [int] IDENTITY(1,1) NOT NULL,
	[Percentage] [decimal](4, 2) NOT NULL,
	[UserCreated] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[UserChanged] [nvarchar](50) NULL,
	[DateChanged] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[FlatTaxRateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FlatValueTaxRate]    Script Date: 2020/07/15 02:10:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlatValueTaxRate](
	[FlatValueTaxRateId] [int] IDENTITY(1,1) NOT NULL,
	[Percentage] [decimal](4, 2) NULL,
	[Value] [money] NULL,
	[MaximumAmount] [money] NOT NULL,
	[UserCreated] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[UserChanged] [nvarchar](50) NULL,
	[DateChanged] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[FlatValueTaxRateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProgressiveTaxRate]    Script Date: 2020/07/15 02:10:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProgressiveTaxRate](
	[ProgressiveTaxRateId] [int] IDENTITY(1,1) NOT NULL,
	[Percentage] [decimal](4, 2) NOT NULL,
	[MinimumAmount] [money] NOT NULL,
	[MaximumAmount] [money] NULL,
	[UserCreated] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[UserChanged] [nvarchar](50) NULL,
	[DateChanged] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProgressiveTaxRateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxAssessment]    Script Date: 2020/07/15 02:10:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxAssessment](
	[TaxAssessmentId] [int] IDENTITY(1,1) NOT NULL,
	[NettIncome] [money] NOT NULL,
	[IncomeTax] [money] NOT NULL,
	[TaxTypeId] [int] NOT NULL,
	[UserCreated] [nvarchar](50) NULL,
	[DateCreated] [datetime2](7) NULL,
	[UserChanged] [nvarchar](50) NULL,
	[DateChanged] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[TaxAssessmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxType]    Script Date: 2020/07/15 02:10:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxType](
	[TaxTypeId] [int] IDENTITY(1,1) NOT NULL,
	[PostalCode] [nvarchar](5) NOT NULL,
	[CalculationType] [nvarchar](20) NOT NULL,
	[UserCreated] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[UserChange] [nvarchar](50) NULL,
	[DateChanged] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[TaxTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[FlatTaxRate] ON 
GO
INSERT [dbo].[FlatTaxRate] ([FlatTaxRateId], [Percentage], [UserCreated], [DateCreated], [UserChanged], [DateChanged]) VALUES (1, CAST(17.50 AS Decimal(4, 2)), N'System', CAST(N'2020-07-13T17:44:38.0133333' AS DateTime2), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[FlatTaxRate] OFF
GO
SET IDENTITY_INSERT [dbo].[FlatValueTaxRate] ON 
GO
INSERT [dbo].[FlatValueTaxRate] ([FlatValueTaxRateId], [Percentage], [Value], [MaximumAmount], [UserCreated], [DateCreated], [UserChanged], [DateChanged]) VALUES (1, CAST(5.00 AS Decimal(4, 2)), 10000.0000, 200000.0000, N'System', CAST(N'2020-07-13T17:50:52.5366667' AS DateTime2), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[FlatValueTaxRate] OFF
GO
SET IDENTITY_INSERT [dbo].[ProgressiveTaxRate] ON 
GO
INSERT [dbo].[ProgressiveTaxRate] ([ProgressiveTaxRateId], [Percentage], [MinimumAmount], [MaximumAmount], [UserCreated], [DateCreated], [UserChanged], [DateChanged]) VALUES (1, CAST(10.00 AS Decimal(4, 2)), 0.0000, 8350.0000, N'System', CAST(N'2020-07-13T17:55:06.6000000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[ProgressiveTaxRate] ([ProgressiveTaxRateId], [Percentage], [MinimumAmount], [MaximumAmount], [UserCreated], [DateCreated], [UserChanged], [DateChanged]) VALUES (2, CAST(15.00 AS Decimal(4, 2)), 8351.0000, 33950.0000, N'System', CAST(N'2020-07-13T17:55:06.6466667' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[ProgressiveTaxRate] ([ProgressiveTaxRateId], [Percentage], [MinimumAmount], [MaximumAmount], [UserCreated], [DateCreated], [UserChanged], [DateChanged]) VALUES (3, CAST(25.00 AS Decimal(4, 2)), 33951.0000, 82250.0000, N'System', CAST(N'2020-07-13T17:55:06.6466667' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[ProgressiveTaxRate] ([ProgressiveTaxRateId], [Percentage], [MinimumAmount], [MaximumAmount], [UserCreated], [DateCreated], [UserChanged], [DateChanged]) VALUES (4, CAST(28.00 AS Decimal(4, 2)), 82251.0000, 171550.0000, N'System', CAST(N'2020-07-13T17:55:06.6466667' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[ProgressiveTaxRate] ([ProgressiveTaxRateId], [Percentage], [MinimumAmount], [MaximumAmount], [UserCreated], [DateCreated], [UserChanged], [DateChanged]) VALUES (5, CAST(33.00 AS Decimal(4, 2)), 171551.0000, 372950.0000, N'System', CAST(N'2020-07-13T17:55:06.6466667' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[ProgressiveTaxRate] ([ProgressiveTaxRateId], [Percentage], [MinimumAmount], [MaximumAmount], [UserCreated], [DateCreated], [UserChanged], [DateChanged]) VALUES (6, CAST(35.00 AS Decimal(4, 2)), 372951.0000, NULL, N'System', CAST(N'2020-07-13T17:55:06.6466667' AS DateTime2), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[ProgressiveTaxRate] OFF
GO
SET IDENTITY_INSERT [dbo].[TaxType] ON 
GO
INSERT [dbo].[TaxType] ([TaxTypeId], [PostalCode], [CalculationType], [UserCreated], [DateCreated], [UserChange], [DateChanged]) VALUES (1, N'7441', N'Progressive', N'System', CAST(N'2020-07-13T18:56:35.3800000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[TaxType] ([TaxTypeId], [PostalCode], [CalculationType], [UserCreated], [DateCreated], [UserChange], [DateChanged]) VALUES (2, N'A100', N'Flat Value', N'System', CAST(N'2020-07-13T18:56:35.4366667' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[TaxType] ([TaxTypeId], [PostalCode], [CalculationType], [UserCreated], [DateCreated], [UserChange], [DateChanged]) VALUES (3, N'7000', N'Flat Rate', N'System', CAST(N'2020-07-13T18:56:35.4400000' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[TaxType] ([TaxTypeId], [PostalCode], [CalculationType], [UserCreated], [DateCreated], [UserChange], [DateChanged]) VALUES (4, N'1000', N'Progressive', N'System', CAST(N'2020-07-13T18:56:35.4400000' AS DateTime2), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TaxType] OFF
GO
ALTER TABLE [dbo].[FlatTaxRate] ADD  DEFAULT ('System') FOR [UserCreated]
GO
ALTER TABLE [dbo].[FlatTaxRate] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[FlatValueTaxRate] ADD  DEFAULT ('System') FOR [UserCreated]
GO
ALTER TABLE [dbo].[FlatValueTaxRate] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[ProgressiveTaxRate] ADD  DEFAULT ('System') FOR [UserCreated]
GO
ALTER TABLE [dbo].[ProgressiveTaxRate] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[TaxType] ADD  DEFAULT ('System') FOR [UserCreated]
GO
ALTER TABLE [dbo].[TaxType] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[TaxAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TaxAssessment_TaxType] FOREIGN KEY([TaxTypeId])
REFERENCES [dbo].[TaxType] ([TaxTypeId])
GO
ALTER TABLE [dbo].[TaxAssessment] CHECK CONSTRAINT [FK_TaxAssessment_TaxType]
GO
USE [master]
GO
ALTER DATABASE [TaxAdministration] SET  READ_WRITE 
GO
