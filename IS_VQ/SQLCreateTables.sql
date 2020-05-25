CREATE TABLE [dbo].[Bookkeeping] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (20) NOT NULL,
    [Surname]  NVARCHAR (20) NOT NULL,
    [Age]      INT           NOT NULL,
    [Salary]   INT           NOT NULL,
    [Projects] INT           NOT NULL
);

CREATE TABLE [dbo].[Customers] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (20) NOT NULL,
    [Surname]  NVARCHAR (20) NOT NULL,
    [Age]      INT           NOT NULL,
    [Salary]   INT           NOT NULL,
    [Projects] INT           NOT NULL
);

CREATE TABLE [dbo].[Logistics] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (20) NOT NULL,
    [Surname]  NVARCHAR (20) NOT NULL,
    [Age]      INT           NOT NULL,
    [Salary]   INT           NOT NULL,
    [Projects] INT           NOT NULL
);

CREATE TABLE [dbo].[Workers] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (20) NOT NULL,
    [Surname]  NVARCHAR (20) NOT NULL,
    [Age]      INT           NOT NULL,
    [Salary]   INT           NOT NULL,
    [Projects] INT           NOT NULL
);
