CREATE DATABASE ContactManager
GO

USE ContactManager
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Contacts] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(150) NOT NULL,
    [PhoneNumber] nvarchar(15) NOT NULL,
    [Email] nvarchar(150) NULL,
    [BirthDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220504235533_Initial', N'3.1.24');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Contacts]') AND [c].[name] = N'BirthDate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Contacts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Contacts] ALTER COLUMN [BirthDate] datetime2 NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220505005716_FixBirthDate', N'3.1.24');

GO

ALTER TABLE [Contacts] ADD [CreationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220505180017_ContactCreationDate', N'3.1.24');

GO

CREATE TABLE [ErrorLogs] (
    [Id] int NOT NULL IDENTITY,
    [DateOccurred] datetime2 NOT NULL,
    [ClasseName] nvarchar(100) NULL,
    [Action] nvarchar(100) NULL,
    [StackTrace] nvarchar(max) NULL,
    [InnerException] nvarchar(max) NULL,
    CONSTRAINT [PK_ErrorLogs] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220505182927_ErrorLog', N'3.1.24');

GO