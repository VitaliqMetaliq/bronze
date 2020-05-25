CREATE TABLE [dbo].[Workers] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [firstname]   NVARCHAR (20)  NOT NULL,
    [lastname]    NVARCHAR (20)  NOT NULL,
    [birthday]    DATE           NOT NULL,
    [address]     NVARCHAR (150) NOT NULL,
    [department]  INT            NOT NULL,
    [Description] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Departments] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (20) NOT NULL
);