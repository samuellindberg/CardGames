CREATE TABLE [dbo].[HighScore] (
    [Id]    INT PRIMARY KEY IDENTITY NOT NULL,
    [Name]  NVARCHAR (50) NOT NULL,
    [HighOrLowScore] INT           NULL,
    [ChicagoScore] INT NULL, 
    UNIQUE NONCLUSTERED ([Name] ASC)
);

