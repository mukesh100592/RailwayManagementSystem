drop database RMS
go

CREATE DATABASE RMS
Go

drop Table Train
go
CREATE TABLE Train(
    Id int IDENTITY(1,1) PRIMARY KEY,
    Name varchar(255) NOT NULL,
    Capacity int default 80 Not Null, --Max Seats 80
    Type int default 0 Not Null, --0=Passenger, 1=Cargo
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
    IsActive bit Default 0,
    IsAdmin bit Default 0);
GO
INSERT INTO [User] (UserName, Password, IsAdmin) VALUES
    ('Krati','krati@gmail',0),
    ('Mukesh','mukesh@gmail',0),
    ('Test','test@gmail',0),    
    ('Admin','Admin@gmail',1);
GO
select * from [User]
GO

drop Table [BookingInfo]
go
CREATE TABLE [BookingInfo](
    Pnr int IDENTITY(1,1) PRIMARY KEY,
    BookingDate DATETIME NOT NULL,
    SourceStation varchar(255) NOT NULL,
    DestinationStation varchar(255) NOT NULL,
    BookingStatus bit Default 1,
    TicketFare int);
GO

select * from [BookingInfo]
GO
