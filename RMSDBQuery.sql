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
Route varchar(255) NOT NULL,
);
GO

INSERT INTO Train (Name, Capacity,Route) VALUES
    ('Train 1',80 ,'Route 1'),
    ('Train 2',80 ,'Route 2'),
    ('Train 3',80 ,'Route 3'),    
    ('Train 4',80 ,'Route 4'),
    ('Train 5',80 ,'Route 5');

select * from Train