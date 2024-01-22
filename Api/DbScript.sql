IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Employees] (
    [Id] int NOT NULL IDENTITY,
    [InstitutionId] int NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Gender] int NULL,
    [JoinDate] datetime2 NOT NULL,
    [Mobile] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [EmployeeType] int NOT NULL,
    [Designation] int NOT NULL,
    [BloodGroup] nvarchar(max) NOT NULL,
    [EmployeeId] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [MotherName] nvarchar(max) NOT NULL,
    [FatherName] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240121222117_Initial', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Countries] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Iso2Code] nvarchar(max) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedBy] int NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [UpdatedBy] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240122221657_Country', N'8.0.0');
GO

COMMIT;
GO

