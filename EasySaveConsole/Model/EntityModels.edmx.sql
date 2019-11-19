
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/18/2019 15:49:26
-- Generated from EDMX file: C:\Users\ccdu2\OneDrive - Association Cesi Viacesi mail\Mes Devoirs\Autres\C#\EasySave\EasySaveConsole\Model\EntityModels.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Travail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Travail];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Travail'
CREATE TABLE [dbo].[Travail] (
    [Id] int  NOT NULL,
    [Name] nvarchar(50)  NULL,
    [SaveType] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Travail'
ALTER TABLE [dbo].[Travail]
ADD CONSTRAINT [PK_Travail]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------