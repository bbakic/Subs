USE [master]
GO
/****** Object:  Database [Modus]    Script Date: 16.08.2021 21:13:27 ******/
CREATE DATABASE [Modus]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Modus', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Modus.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Modus_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Modus_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Modus] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Modus].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Modus] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Modus] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Modus] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Modus] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Modus] SET ARITHABORT OFF 
GO
ALTER DATABASE [Modus] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Modus] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Modus] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Modus] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Modus] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Modus] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Modus] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Modus] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Modus] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Modus] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Modus] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Modus] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Modus] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Modus] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Modus] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Modus] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Modus] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Modus] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Modus] SET  MULTI_USER 
GO
ALTER DATABASE [Modus] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Modus] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Modus] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Modus] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Modus] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Modus] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Modus] SET QUERY_STORE = OFF
GO
USE [Modus]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](50) NOT NULL,
	[Postcode] [nvarchar](10) NOT NULL,
	[City] [nvarchar](30) NOT NULL,
	[Country] [nvarchar](30) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Oib] [nvarchar](11) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExpenseItems]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpenseItems](
	[Id] [int] NOT NULL,
	[ExpenseId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[Quantity] [decimal](8, 3) NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[Amount] [decimal](16, 2) NOT NULL,
 CONSTRAINT [PK_ExpenseItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expenses]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenses](
	[Id] [int] NOT NULL,
	[JournalId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JournalItems]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalItems](
	[Id] [int] NOT NULL,
	[JournalId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Quantity] [decimal](8, 3) NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[DiscountPercent] [decimal](6, 3) NOT NULL,
 CONSTRAINT [PK_JournalItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Journals]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Journals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ServiceCategoryId] [int] NOT NULL,
	[CreateDate] [date] NOT NULL,
	[ExpiryDate] [date] NOT NULL,
	[FlatAmount] [decimal](8, 2) NOT NULL,
 CONSTRAINT [PK_SubscriptionJournals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceCategories]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsPrepaid] [bit] NOT NULL,
	[Duration] [int] NOT NULL,
	[UnitQuantity] [int] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceCategoryId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceTiers]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceTiers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceId] [int] NOT NULL,
	[MinQuantity] [int] NOT NULL,
	[MaxQuantity] [int] NULL,
	[Price] [decimal](8, 2) NOT NULL,
 CONSTRAINT [PK_ServicePrices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubscriptionItems]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[DiscountPercent] [decimal](6, 3) NOT NULL,
 CONSTRAINT [PK_SubscriptionItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subscriptions]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ServiceCategoryId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[ExpiryDate] [date] NOT NULL,
	[RenewalDayOfMonth] [int] NULL,
	[IsAutomaticRenewal] [bit] NOT NULL,
	[FlatAmount] [decimal](8, 2) NOT NULL,
 CONSTRAINT [PK_Subscriptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubsVersionItems]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubsVersionItems](
	[Id] [int] NOT NULL,
	[SubsVersionId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[DiscountPercent] [decimal](6, 3) NOT NULL,
 CONSTRAINT [PK_SubsVersionItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubsVersions]    Script Date: 16.08.2021 21:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubsVersions](
	[Id] [int] NOT NULL,
	[SubscriptionId] [int] NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[IsAfterExpiry] [bit] NOT NULL,
 CONSTRAINT [PK_SubsVersions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ExpenseItems]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseItems_Expenses] FOREIGN KEY([ExpenseId])
REFERENCES [dbo].[Expenses] ([Id])
GO
ALTER TABLE [dbo].[ExpenseItems] CHECK CONSTRAINT [FK_ExpenseItems_Expenses]
GO
ALTER TABLE [dbo].[ExpenseItems]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseItems_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[ExpenseItems] CHECK CONSTRAINT [FK_ExpenseItems_Services]
GO
ALTER TABLE [dbo].[Expenses]  WITH CHECK ADD  CONSTRAINT [FK_Expenses_Journals] FOREIGN KEY([JournalId])
REFERENCES [dbo].[Journals] ([Id])
GO
ALTER TABLE [dbo].[Expenses] CHECK CONSTRAINT [FK_Expenses_Journals]
GO
ALTER TABLE [dbo].[JournalItems]  WITH CHECK ADD  CONSTRAINT [FK_JournalItems_Journals] FOREIGN KEY([JournalId])
REFERENCES [dbo].[Journals] ([Id])
GO
ALTER TABLE [dbo].[JournalItems] CHECK CONSTRAINT [FK_JournalItems_Journals]
GO
ALTER TABLE [dbo].[JournalItems]  WITH CHECK ADD  CONSTRAINT [FK_JournalItems_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[JournalItems] CHECK CONSTRAINT [FK_JournalItems_Services]
GO
ALTER TABLE [dbo].[Journals]  WITH CHECK ADD  CONSTRAINT [FK_Journals_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Journals] CHECK CONSTRAINT [FK_Journals_Customers]
GO
ALTER TABLE [dbo].[Journals]  WITH CHECK ADD  CONSTRAINT [FK_Journals_ServiceCategories] FOREIGN KEY([ServiceCategoryId])
REFERENCES [dbo].[ServiceCategories] ([Id])
GO
ALTER TABLE [dbo].[Journals] CHECK CONSTRAINT [FK_Journals_ServiceCategories]
GO
ALTER TABLE [dbo].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_ServiceCategories] FOREIGN KEY([ServiceCategoryId])
REFERENCES [dbo].[ServiceCategories] ([Id])
GO
ALTER TABLE [dbo].[Services] CHECK CONSTRAINT [FK_Services_ServiceCategories]
GO
ALTER TABLE [dbo].[ServiceTiers]  WITH CHECK ADD  CONSTRAINT [FK_ServiceTiers_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[ServiceTiers] CHECK CONSTRAINT [FK_ServiceTiers_Services]
GO
ALTER TABLE [dbo].[SubscriptionItems]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionItems_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[SubscriptionItems] CHECK CONSTRAINT [FK_SubscriptionItems_Services]
GO
ALTER TABLE [dbo].[SubscriptionItems]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionItems_Subscriptions] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscriptions] ([Id])
GO
ALTER TABLE [dbo].[SubscriptionItems] CHECK CONSTRAINT [FK_SubscriptionItems_Subscriptions]
GO
ALTER TABLE [dbo].[Subscriptions]  WITH CHECK ADD  CONSTRAINT [FK_Subscriptions_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Subscriptions] CHECK CONSTRAINT [FK_Subscriptions_Customers]
GO
ALTER TABLE [dbo].[Subscriptions]  WITH CHECK ADD  CONSTRAINT [FK_Subscriptions_ServiceCategories] FOREIGN KEY([ServiceCategoryId])
REFERENCES [dbo].[ServiceCategories] ([Id])
GO
ALTER TABLE [dbo].[Subscriptions] CHECK CONSTRAINT [FK_Subscriptions_ServiceCategories]
GO
ALTER TABLE [dbo].[SubsVersionItems]  WITH CHECK ADD  CONSTRAINT [FK_SubsVersionItems_Services] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[SubsVersionItems] CHECK CONSTRAINT [FK_SubsVersionItems_Services]
GO
ALTER TABLE [dbo].[SubsVersionItems]  WITH CHECK ADD  CONSTRAINT [FK_SubsVersionItems_SubsVersions] FOREIGN KEY([SubsVersionId])
REFERENCES [dbo].[SubsVersions] ([Id])
GO
ALTER TABLE [dbo].[SubsVersionItems] CHECK CONSTRAINT [FK_SubsVersionItems_SubsVersions]
GO
ALTER TABLE [dbo].[SubsVersions]  WITH CHECK ADD  CONSTRAINT [FK_SubsVersions_Subscriptions] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[Subscriptions] ([Id])
GO
ALTER TABLE [dbo].[SubsVersions] CHECK CONSTRAINT [FK_SubsVersions_Subscriptions]
GO
USE [master]
GO
ALTER DATABASE [Modus] SET  READ_WRITE 
GO
