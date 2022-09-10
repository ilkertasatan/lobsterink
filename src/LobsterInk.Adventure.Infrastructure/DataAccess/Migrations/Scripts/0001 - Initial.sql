IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Adventure')
BEGIN
    CREATE DATABASE Adventure
END
GO
