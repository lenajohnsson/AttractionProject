USE [sql-Attractions];
GO

-- Create a gstusr-schema
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'gstusr')
    EXEC('CREATE SCHEMA gstusr');
GO

-- Stored procedure to delete all seeded data
CREATE OR ALTER PROC dbo.spDeleteAll
    @seededParam BIT = 1

    AS

    SET NOCOUNT ON;

    DELETE FROM dbo.Addresses WHERE Seeded = @seededParam;
    DELETE FROM dbo.Users WHERE Seeded = @seededParam;
    DELETE FROM dbo.Categories WHERE Seeded = @seededParam;
    DELETE FROM dbo.Reviews WHERE Seeded = @seededParam;
    DELETE FROM dbo.Attractions WHERE Seeded = @seededParam;

GO

-- View for an overview
CREATE OR ALTER VIEW gstusr.vwInfoDb AS
    SELECT  (SELECT COUNT(*) FROM dbo.Users WHERE Seeded = 1) as nrSeededUsers,
            (SELECT COUNT(DISTINCT City) FROM dbo.Addresses WHERE Seeded = 1) as nrSeededCities,
            (SELECT COUNT(*) FROM dbo.Attractions WHERE Seeded = 1) as nrSeededAttractions;
GO