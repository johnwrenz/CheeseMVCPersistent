USE [CheeseMVC]
GO

/****** Object: Table [dbo].[Cheeses] Script Date: 12/16/2017 11:28:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cheeses] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [Type]        INT            NOT NULL,
	constraint Categories foreign key (ID)
);


