# KataRepository
Project to read a plain text file to upload trips from different drivers

According to the Code Exercise:
--------------------------------
Code Kata
Use .NET for solution. The goal of this exercise is to get a better glimpse into your
thought process. While this is a simple exercise, think of it as a large project, so put
whatever patterns you think would be necessary for an enterprise level application.
Create a ReadMe file to explain any details you think would help verify your work.
Submit solution to GitHub or any other publicly accessible code repository.
The code will process an input file.
Each line in the input file will start with a command. There are two possible commands.
The first command is Driver, which will register a new Driver in the app. Example: Driver
Dan The second command is Trip, which will record a trip attributed to a driver. The line
will be space delimited with the following fields: the command (Trip), driver name, start
time, stop time, miles driven. Times will be given in the format of hours:minutes. We'll
use a 24-hour clock and will assume that drivers never drive past midnight (the start
time will always be before the end time). Example: Trip Dan 07:15 07:45 17.3 Discard any
trips that average a speed of less than 5 mph or greater than 100 mph. Generate a
report containing each driver with total miles driven and average speed. Sort the output
by most miles driven to least. Round miles and miles per hour to the nearest integer.
Example input:
Driver Dan
Driver Alex
Driver Bob
Trip Dan 07:15 07:45 17.3
Trip Dan 06:12 06:32 21.8
Trip Alex 12:01 13:16 42.0
Expected output:
Alex: 42 miles @ 34 mph
Dan: 39 miles @ 47 mph
Bob: 0 miles 
--------------------------------

This Solution has the following structure:

1.- Front End:	

	1.1- Client App  
	
2.- Back End:

	2.1- Service Layer
  
  	2.2- Business Layer
  
  	2.3- Data Access Layer
  
  	2.3- ORM
  
  	2.4- Business Model
  
  	2.5- Dto
  
  
  The complete solution integrates a Client Server Arquitecture within a Monolithic N-Layer - 1 Tier Structure patten.
  
  The Front End:
  
    The Client App is a Web App .Net core 3.1 MVC.
  The Back End:
  
    	The Service Layer is a Web Api restful .Net core 3.1
    	With .Net core 3.1 Class libraries for BL, DAL, ORM, Business Model and Dto.
  
  The ORM used is Entity FrameWork .Net core "Code First" Approach with Fluent API (Not Migrations).
  
  The Solution was concibed to be deployed in a Client-Server Architecture with an N-Layer 1 Tier Architecture pattern.
  Applying the separation of concern principle, the solution have a light Service layer to attend and dispatch the requests comming from the Client.
  The Back End uses the Unit of Work pattern working in conjuction with Respositories to create a middle tier between the BL and the DAL to uncopled Business Logic fron Data.
  
  The DAL uses the ORM EntityFramework to act as a wrapper over the DB for persistence.
  
  And finally the Web Api Restful communicates with the Client app using DTO's.
    
  CONSIDERATIONS TO GOOD FUNCTIONING OF THE SOLUTION
  
  	1.- Run the Data Base Creational SQL Script in order to generate the Db(Please refer to this script at the end of this read me file).
  	2.- After the DB is created, it is important to update the appsetting.json file within the Web API Restful app with the correct settings for the Db connection string.
  	3.- When the Web APi restful app is up and running copy the "localhost" URL provided for this app and update the following line within the Client App (HomeController/UploadTrips):
  	var response = await client.PostAsync("http://localhost:52131/" + "UploadTrips", form); located at line 74.
  
  
  
  Thanks, for the opportunity.
  Kind regards!
  
  
  
  
  
  
  ------------------
  Data Base Script
  ------------------
  
  Copy from here........
  
USE [master]
GO

/****** Object:  Database [KataDb]  ******/
CREATE DATABASE [KataDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KataDb', FILENAME = N'C:\KataDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KataDb_log', FILENAME = N'C:\KataDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KataDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [KataDb] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [KataDb] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [KataDb] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [KataDb] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [KataDb] SET ARITHABORT OFF 
GO

ALTER DATABASE [KataDb] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [KataDb] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [KataDb] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [KataDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [KataDb] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [KataDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [KataDb] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [KataDb] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [KataDb] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [KataDb] SET  DISABLE_BROKER 
GO

ALTER DATABASE [KataDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [KataDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [KataDb] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [KataDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [KataDb] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [KataDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [KataDb] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [KataDb] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [KataDb] SET  MULTI_USER 
GO

ALTER DATABASE [KataDb] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [KataDb] SET DB_CHAINING OFF 
GO

ALTER DATABASE [KataDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [KataDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [KataDb] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [KataDb] SET QUERY_STORE = OFF
GO

USE [KataDb]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE [KataDb] SET  READ_WRITE 
GO




USE [KataDb]
GO

/****** Object:  Table [dbo].[File]    Script Date: 11/29/2020 3:07:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[File](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



USE [KataDb]
GO

/****** Object:  Table [dbo].[Driver]    Script Date: 11/29/2020 3:07:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Driver](
	[DriverId] [int] IDENTITY(1,1) NOT NULL,
	[DriverName] [nvarchar](50) NOT NULL,
	[FileId] [int] NOT NULL,
 CONSTRAINT [PK_Driver] PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Driver]  WITH CHECK ADD  CONSTRAINT [FK_Driver_File] FOREIGN KEY([FileId])
REFERENCES [dbo].[File] ([FileId])
GO

ALTER TABLE [dbo].[Driver] CHECK CONSTRAINT [FK_Driver_File]
GO




USE [KataDb]
GO

/****** Object:  Table [dbo].[Trip]    Script Date: 11/29/2020 3:07:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Trip](
	[TripId] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Miles] [int] NOT NULL,
	[AvgMph] [int] NOT NULL,
	[TripDuration] [decimal](18, 2) NOT NULL,
	[DriverId] [int] NOT NULL,
	[FileId] [int] NOT NULL,
 CONSTRAINT [PK_Trip] PRIMARY KEY CLUSTERED 
(
	[TripId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Trip]  WITH CHECK ADD  CONSTRAINT [FK_Trip_Driver] FOREIGN KEY([DriverId])
REFERENCES [dbo].[Driver] ([DriverId])
GO

ALTER TABLE [dbo].[Trip] CHECK CONSTRAINT [FK_Trip_Driver]
GO

ALTER TABLE [dbo].[Trip]  WITH CHECK ADD  CONSTRAINT [FK_Trip_File] FOREIGN KEY([FileId])
REFERENCES [dbo].[File] ([FileId])
GO

ALTER TABLE [dbo].[Trip] CHECK CONSTRAINT [FK_Trip_File]
GO

To here.......
