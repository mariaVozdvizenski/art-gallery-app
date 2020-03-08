-- "" unconventional variable name in quotes
-- '' strings in single quotes
-- GO documentation - https://docs.microsoft.com/en-us/sql/t-sql/language-elements/sql-server-utilities-statements-go?view=sql-server-ver15

IF db_id('mavozd_softdelete') IS NOT NULL BEGIN
    USE master
    DROP DATABASE "mavozd_softdelete"
END
GO

CREATE DATABASE "mavozd_softdelete"
GO

USE "mavozd_softdelete"
GO

-- CRUD with a single table 

DROP TABLE Artist

-- Create basic table structure
CREATE TABLE Artist (
    Id INT NOT NULL IDENTITY,
    Name    VARCHAR(128)    NOT NULL,
    PlaceOfBirth VARCHAR(128) NOT NULL,
    Country VARCHAR(50) NOT NULL,
    DateOfBirth DATETIME2 NOT NULL,
    DateDeceased DATETIME2 NULL,
    DeletedAt DATETIME2,
    CreatedAt DATETIME2
    CONSTRAINT PK_Artist PRIMARY KEY (Id, DeletedAt)
)
-- add index on metadata
CREATE INDEX DeletedAt_idx ON "Artist" ( DeletedAt );
CREATE INDEX CreatedAt_idx ON "Artist" ( CreatedAt );


-- soft delete------------------------------------------
INSERT INTO Artist (Name, PlaceOfBirth, Country, DateOfBirth, CreatedAt, DeletedAt) VALUES ('Foo', 'Tallinn, Estonia', 'Italy', '1998-01-01', '2020-02-01', '9999-12-12')
INSERT INTO Artist (Name, PlaceOfBirth, Country, DateOfBirth, DateDeceased, CreatedAt, DeletedAt) VALUES ('Bar', 'Tartu, Estonia', 'Romania', '1800-01-01','1888-02-02', '2020-02-01', '9999-12-12')

SELECT 'Initial data before soft delete'
SELECT * FROM Artist

DECLARE @Time2 DATETIME2
SELECT @Time2 = '2020-04-02'

-- instead of
-- DELETE FROM Person WHERE Name like '%a%'
-- update rows set timestamp to DeletedAt

UPDATE Artist SET DeletedAt=@Time2 WHERE Name like '%a%'

SELECT 'Full Data after soft delete'
SELECT * FROM Artist

DECLARE @TimeNow DATETIME2
SELECT @TimeNow = '2020-05-02'

SELECT 'Correct Data after soft delete'
SELECT * FROM Artist WHERE  CreatedAt <= @TimeNow AND (DeletedAt IS NULL OR DeletedAt > @TimeNow)

-- soft update-----------------------------------------------------------------------------------
DROP TABLE Artist

CREATE TABLE Artist (
    Id INT NOT NULL IDENTITY,
    Name    VARCHAR(128)    NOT NULL,
    PlaceOfBirth VARCHAR(128) NOT NULL,
    Country VARCHAR(50) NOT NULL,
    DateOfBirth DATETIME2 NOT NULL,
    DateDeceased DATETIME2 NULL,
    DeletedAt DATETIME2,
    CreatedAt DATETIME2
    CONSTRAINT PK_Artist PRIMARY KEY (Id, DeletedAt)
)
-- add index on metadata
CREATE INDEX DeletedAt_idx ON "Artist" ( DeletedAt );
CREATE INDEX CreatedAt_idx ON "Artist" ( CreatedAt );

INSERT INTO Artist (Name, PlaceOfBirth, Country, DateOfBirth, CreatedAt, DeletedAt) VALUES ('Foo', 'Tallinn, Estonia', 'Italy', '1998-01-01', '2020-02-01', '9999-12-12')
INSERT INTO Artist (Name, PlaceOfBirth, Country, DateOfBirth, DateDeceased, CreatedAt, DeletedAt) VALUES ('Bar', 'Tartu, Estonia', 'Romania', '1800-01-01','1888-02-02', '2020-02-01', '9999-12-12')

SELECT 'Data before soft update 1'
SELECT * FROM Artist

DECLARE @Time3 DATETIME2
SELECT @Time3 = '2020-03-03'

DECLARE @TimeFuture DATETIME2
SELECT @TimeFuture = '9999-12-12'

-- create a copy of initial record
-- get the id
DECLARE @OriginalId INT
SELECT @OriginalId = Id FROM Artist Where Name like 'Foo'

-- Set IDENTITY_INSERT to ON so that we can set the Id ourselves
SET IDENTITY_INSERT Artist ON 

-- Update 1
UPDATE Artist SET DeletedAt = @Time3 WHERE Id = @OriginalId AND DeletedAt = @TimeFuture
INSERT INTO Artist (Id, Name, PlaceOfBirth, Country, DateOfBirth, DateDeceased, CreatedAt, DeletedAt) SELECT Id, 'Fido', PlaceOfBirth, Country, DateOfBirth, DateDeceased, CreatedAt, @TimeFuture FROM Artist where Id = @OriginalId

SET IDENTITY_INSERT Artist OFF 

SELECT 'Full Data after soft update 1'
SELECT * FROM Artist

-- Update 2
DECLARE @Time4 DATETIME2
SELECT @Time4 = '2020-05-03'

SET IDENTITY_INSERT Artist ON 

UPDATE Artist SET DeletedAt = @Time4 WHERE Id = @OriginalId AND DeletedAt = @TimeFuture
INSERT INTO Artist (Id, Name, PlaceOfBirth, Country, DateOfBirth, DateDeceased, CreatedAt, DeletedAt) SELECT Id, 'Fefe', PlaceOfBirth, Country, DateOfBirth, DateDeceased, CreatedAt, @TimeFuture FROM Artist where Id = @OriginalId AND DeletedAt = @Time4

SET IDENTITY_INSERT Artist OFF 

SELECT 'Full Data after soft update 2'
SELECT * FROM Artist

DECLARE @TimeNow1 DATETIME2
SELECT @TimeNow1 = '2020-07-03'

SELECT 'Correct Data after soft update'
SELECT * FROM Artist WHERE  CreatedAt <= @TimeNow1 AND DeletedAt > @TimeNow1

--------------------------------------------------------------------------------------------------------

-- CRUD with 1:m 
DROP TABLE Painting
DROP TABLE Artist

CREATE TABLE Artist (
    Id INT NOT NULL IDENTITY,
    Name    VARCHAR(128)    NOT NULL,
    PlaceOfBirth VARCHAR(128) NOT NULL,
    Country VARCHAR(50) NOT NULL,
    DateOfBirth DATETIME2 NOT NULL,
    DateDeceased DATETIME2 NULL,
    DeletedAt DATETIME2,
    CreatedAt DATETIME2
    CONSTRAINT PK_Artist PRIMARY KEY (Id, DeletedAt)
)
-- add index on metadata
CREATE INDEX DeletedAt_idx ON "Artist" ( DeletedAt );
CREATE INDEX CreatedAt_idx ON "Artist" ( CreatedAt );

DELETE FROM Artist

INSERT INTO Artist (Name, PlaceOfBirth, Country, DateOfBirth, CreatedAt, DeletedAt) VALUES ('Foo', 'Tallinn, Estonia', 'Italy', '1998-01-01', '2020-02-01', '9999-12-12')
INSERT INTO Artist (Name, PlaceOfBirth, Country, DateOfBirth, DateDeceased, CreatedAt, DeletedAt) VALUES ('Bar', 'Tartu, Estonia', 'Romania', '1800-01-01','1888-02-02', '2020-02-01', '9999-12-12')

SELECT 'Initial soft data for Artist'
SELECT * FROM Artist

CREATE TABLE Painting (
    Id INT NOT NULL IDENTITY,
    ArtistId INT NOT NULL,
    ArtistDeletedAt DATETIME2 NOT NULL,
    Description  VARCHAR(496) NULL,
    Price Decimal(19,4) NOT NULL,
    Title VARCHAR(128) NOT NULL,
    Size VARCHAR(36) NOT NULL,
    DeletedAt DATETIME2,
    CreatedAt DATETIME2,
    CONSTRAINT PK_Painting PRIMARY KEY (Id, DeletedAt),
)
Alter TABLE Painting WITH CHECK ADD CONSTRAINT FK_Painting_Artist FOREIGN KEY (ArtistId, ArtistDeletedAt ) REFERENCES Artist (Id, DeletedAt) ON UPDATE CASCADE

CREATE INDEX DeletedAt_idx ON "Painting" ( DeletedAt );
CREATE INDEX CreatedAt_idx ON "Painting" ( CreatedAt );

INSERT INTO Painting (ArtistId, ArtistDeletedAt, Description, Price, Title, Size, CreatedAt, DeletedAt) VALUES (1, '9999-12-12', 'Mural on canvas.', 10.3, 'Dreamy', '20x20cm', '2020-03-03', '9999-12-12')
INSERT INTO Painting (ArtistId, ArtistDeletedAt, Description, Price, Title, Size, CreatedAt, DeletedAt) VALUES (2, '9999-12-12', 'Painted in nature.', 45, '3os', '40x50cm', '2020-03-01', '9999-12-12')

SELECT 'Initial Soft data for Painting'
SELECT * FROM Painting

-------- Soft update master table (Artist)-------------------------------------------

DECLARE @Time6 DATETIME2
SELECT @Time6 = '2020-03-03'

DECLARE @TimeFuture2 DATETIME2
SELECT @TimeFuture2 = '9999-12-12'

-- create a copy of initial record
-- get the id
DECLARE @OriginalId2 INT
SELECT @OriginalId2 = Id FROM Artist Where Name like 'Foo'

-- Set IDENTITY_INSERT to ON so that we can set the Id ourselves
SET IDENTITY_INSERT Artist ON 

-- Update the master
UPDATE Artist SET DeletedAt = @Time6 WHERE Id = @OriginalId2 AND DeletedAt = @TimeFuture2
INSERT INTO Artist (Id, Name, PlaceOfBirth, Country, DateOfBirth, DateDeceased, CreatedAt, DeletedAt) SELECT Id, 'Fido', PlaceOfBirth, Country, DateOfBirth, DateDeceased, CreatedAt, @TimeFuture2 FROM Artist where Id = @OriginalId2 AND DeletedAt = @Time6

SET IDENTITY_INSERT Artist OFF 

SELECT 'Master table after soft update'
SELECT * FROM Artist

--Update the child record
--Set the child as deleted
SET IDENTITY_INSERT Painting ON 

UPDATE Painting SET DeletedAt = @Time6 WHERE Id = @OriginalId2 AND DeletedAt = @TimeFuture2
INSERT INTO Painting (Id, ArtistId, ArtistDeletedAt, [Description], Price, Title, [Size], DeletedAt, CreatedAt ) SELECT Id, ArtistId, @TimeFuture2, [Description], Price, Title, [Size], @TimeFuture2, CreatedAt FROM Painting where ArtistId = @OriginalId2 AND DeletedAt = @Time6

SET IDENTITY_INSERT Painting OFF 

SELECT 'Child table after soft update'
SELECT * FROM Painting

-- Joins

SELECT 'Correct data after soft update'
SELECT Title, Price, Artist.Name, Description
FROM Painting
INNER JOIN Artist ON Artist.Id = Painting.ArtistId AND Artist.DeletedAt = '9999-12-12'

------ Soft Update child table (Painting)-------------------

DECLARE @Time7 DATETIME2
SELECT @Time7 = '2020-06-03'

DECLARE @TimeFuture3 DATETIME2
SELECT @TimeFuture3 = '9999-12-12'

DECLARE @OriginalId3 INT
SELECT @OriginalId3 = Id FROM Painting Where Title like 'Dreamy'

SET IDENTITY_INSERT Painting ON 

UPDATE Painting SET DeletedAt = @Time7 WHERE Id = @OriginalId3 AND DeletedAt = @TimeFuture3
INSERT INTO Painting (Id, ArtistId, ArtistDeletedAt, [Description], Price, Title, [Size], DeletedAt, CreatedAt ) SELECT Id, ArtistId, @TimeFuture3, [Description], 500, Title, [Size], @TimeFuture3, CreatedAt FROM Painting where Id = @OriginalId3 AND DeletedAt = @Time7

SET IDENTITY_INSERT Painting OFF 

SELECT 'Child table after soft update'
SELECT * FROM Painting

-------------- Soft delete master table (Artist)
SELECT 'Data before soft delete'
SELECT * FROM Artist

DECLARE @Time8 DATETIME2
SELECT @Time8 = '2020-08-03'

DECLARE @OriginalId4 INT
SELECT @OriginalId4 = Id FROM Artist Where Name like 'Bar'

-- Deleting the master record
UPDATE Artist SET DeletedAt = @Time8 WHERE Id = @OriginalId4 AND DeletedAt = '9999-12-12'

-- Deleting the child record
UPDATE Painting SET DeletedAt = @Time8 WHERE ArtistId = @OriginalId4 AND ArtistDeletedAt = @Time8

SELECT 'Data after soft delete'
SELECT * FROM Painting

------------------------------------------------------------------------------------

