drop database RMS
go

CREATE DATABASE RMS
Go

drop Table Train
go
CREATE TABLE Train(
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name varchar(255) NOT NULL,
    Capacity int default 80 Not Null, --Max Seats 40
    Type int default 0 Not Null, --0=Passenger, 1=Goods
    Route varchar(255) NOT NULL);
GO
ALTER TABLE Train DROP COLUMN Type;
Go
ALTER TABLE Train ADD Type int DEFAULT 0 Not Null;
Go
INSERT INTO Train (Name, Capacity,Route) VALUES
    ('Train 1',80 ,'Route 1'),
    ('Train 2',80 ,'Route 2'),
    ('Train 3',80 ,'Route 3'),    
    ('Train 4',80 ,'Route 4'),
    ('Train 5',80 ,'Route 5');
GO
select * from Train
GO

drop Table Station
go
CREATE TABLE Station(
    Id int IDENTITY(1,1) PRIMARY KEY,
    Code varchar(255) NOT NULL,
    City varchar(255) NOT NULL);
GO
INSERT INTO Station (Code, City) VALUES
    ('NDLS','New Delhi'),
    ('CNB','Kanpur'),
    ('BNS','Banaras'),    
    ('PNBE','Patna'),
    ('HWH','Kolkata');
GO
select * from Station;
GO

drop Table [User]
go
CREATE TABLE [User](
    Id int IDENTITY(1,1) PRIMARY KEY,
    UserName varchar(255) NOT NULL,
    Password varchar(255) NOT NULL,
    IsActive bit Default 1);
GO
INSERT INTO [User] (UserName, Password) VALUES
    ('Krati','krati@gmail'),
    ('Mukesh','mukesh@gmail'),
    ('Test','test@gmail'),    
    ('Admin','Admin@gmail');
GO
select * from [User]
GO