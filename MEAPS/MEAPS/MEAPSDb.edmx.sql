
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/04/2016 21:52:09
-- Generated from EDMX file: C:\Users\Rangan\Documents\Visual Studio 2015\Projects\MEAPS\MEAPS\MEAPSDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MEAPSDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Attendances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attendances];
GO
IF OBJECT_ID(N'[dbo].[CSVDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CSVDatas];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[ExceptionAttendances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExceptionAttendances];
GO
IF OBJECT_ID(N'[dbo].[Holidays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Holidays];
GO
IF OBJECT_ID(N'[dbo].[LeaveApplications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeaveApplications];
GO
IF OBJECT_ID(N'[dbo].[LeaveStatuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeaveStatuses];
GO
IF OBJECT_ID(N'[dbo].[TimeExceptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TimeExceptions];
GO
IF OBJECT_ID(N'[dbo].[TabCtrls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TabCtrls];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Attendances'
CREATE TABLE [dbo].[Attendances] (
    [SISOId] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [InTime] time  NOT NULL,
    [OutTime] time  NOT NULL,
    [Employee_EmpId] int  NOT NULL
);
GO

-- Creating table 'CSVDatas'
CREATE TABLE [dbo].[CSVDatas] (
    [Serial] int IDENTITY(1,1) NOT NULL,
    [Date] nvarchar(max)  NOT NULL,
    [Dept] nvarchar(max)  NOT NULL,
    [EmpId] nvarchar(max)  NOT NULL,
    [In1] nvarchar(max)  NOT NULL,
    [Out1] nvarchar(max)  NULL,
    [In2] nvarchar(max)  NULL,
    [Out2] nvarchar(max)  NULL,
    [In3] nvarchar(max)  NULL,
    [Out3] nvarchar(max)  NULL,
    [In4] nvarchar(max)  NULL,
    [Out4] nvarchar(max)  NULL,
    [In5] nvarchar(max)  NULL,
    [Out5] nvarchar(max)  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmpId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Dept] nvarchar(max)  NOT NULL,
    [JoiningDate] datetime  NOT NULL,
    [CL] float  NOT NULL,
    [ML] float  NOT NULL,
    [EL] float  NOT NULL,
    [LWP] float  NOT NULL
);
GO

-- Creating table 'ExceptionAttendances'
CREATE TABLE [dbo].[ExceptionAttendances] (
    [EmpId] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Remarks] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Holidays'
CREATE TABLE [dbo].[Holidays] (
    [Date] datetime  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LeaveApplications'
CREATE TABLE [dbo].[LeaveApplications] (
    [ApplicationId] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Remarks] nvarchar(max)  NOT NULL,
    [EmployeeEmpId] int  NOT NULL
);
GO

-- Creating table 'LeaveStatuses'
CREATE TABLE [dbo].[LeaveStatuses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [QCL_In] int  NULL,
    [QCL_Out] int  NULL,
    [HCL] int  NULL,
    [FCL] int  NULL,
    [HML] int  NULL,
    [FML] int  NULL,
    [EL] int  NULL,
    [PresenceState] nvarchar(max)  NOT NULL,
    [EmployeeEmpId] int  NOT NULL,
    [LWP] float  NOT NULL
);
GO

-- Creating table 'TimeExceptions'
CREATE TABLE [dbo].[TimeExceptions] (
    [Date] datetime  NOT NULL,
    [InTime] time  NOT NULL,
    [OutTime] time  NOT NULL
);
GO

-- Creating table 'TabCtrls'
CREATE TABLE [dbo].[TabCtrls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [selectedIndex] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [SISOId] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [PK_Attendances]
    PRIMARY KEY CLUSTERED ([SISOId] ASC);
GO

-- Creating primary key on [Serial] in table 'CSVDatas'
ALTER TABLE [dbo].[CSVDatas]
ADD CONSTRAINT [PK_CSVDatas]
    PRIMARY KEY CLUSTERED ([Serial] ASC);
GO

-- Creating primary key on [EmpId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmpId] ASC);
GO

-- Creating primary key on [EmpId] in table 'ExceptionAttendances'
ALTER TABLE [dbo].[ExceptionAttendances]
ADD CONSTRAINT [PK_ExceptionAttendances]
    PRIMARY KEY CLUSTERED ([EmpId] ASC);
GO

-- Creating primary key on [Date] in table 'Holidays'
ALTER TABLE [dbo].[Holidays]
ADD CONSTRAINT [PK_Holidays]
    PRIMARY KEY CLUSTERED ([Date] ASC);
GO

-- Creating primary key on [ApplicationId] in table 'LeaveApplications'
ALTER TABLE [dbo].[LeaveApplications]
ADD CONSTRAINT [PK_LeaveApplications]
    PRIMARY KEY CLUSTERED ([ApplicationId] ASC);
GO

-- Creating primary key on [Id] in table 'LeaveStatuses'
ALTER TABLE [dbo].[LeaveStatuses]
ADD CONSTRAINT [PK_LeaveStatuses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Date] in table 'TimeExceptions'
ALTER TABLE [dbo].[TimeExceptions]
ADD CONSTRAINT [PK_TimeExceptions]
    PRIMARY KEY CLUSTERED ([Date] ASC);
GO

-- Creating primary key on [Id] in table 'TabCtrls'
ALTER TABLE [dbo].[TabCtrls]
ADD CONSTRAINT [PK_TabCtrls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------