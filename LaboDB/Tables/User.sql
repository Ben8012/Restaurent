﻿CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [firstname] NVARCHAR(50) NOT NULL,
    [lastname] NVARCHAR(50) NOT NULL,
    [email] NVARCHAR(384) NOT NULL UNIQUE,
    [passwd] NVARCHAR(300) NOT NULL,
    [phone] NVARCHAR(50) NULL,
    [role] NVARCHAR(50) NOT NULL,
    [birthDate] DATETIME2 NOT NULL,
    [createdAt] DATETIME2 NOT NULL,
    [updatedAt] DATETIME2 NULL,
    [isActive] BIT NOT NULL,
    [adress_Id] INT NOT NULL, 
    CONSTRAINT [FK_User_ToTable] FOREIGN KEY ([adress_Id]) REFERENCES [Adress]([Id])
   
)
