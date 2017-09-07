Create database OhSnap;

go

CREATE TABLE OhSnap.dbo.Roles (
    RoleID int IDENTITY(1,1) PRIMARY KEY,
    RoleName nvarchar(10) UNIQUE NOT NULL
); 

go

insert into OhSnap.dbo.Roles (RoleName) values ('Admin');
insert into OhSnap.dbo.Roles (RoleName) values ('User');

go

CREATE TABLE OhSnap.dbo.Users (
    UserID int IDENTITY(1,1) PRIMARY KEY,
	UserLogin nvarchar(50) UNIQUE NOT NULL,
	UserPassword varbinary(max) NOT NULL,
	Email nvarchar(50) UNIQUE NOT NULL,
    FirstName nvarchar(50) NOT NULL,
    LastName nvarchar(50) NOT NULL,
	RoleID int FOREIGN KEY REFERENCES OhSnap.dbo.Roles(RoleID) NOT NULL,
	Country nvarchar(50),
	City nvarchar(50),
	CreationDate datetime
); 

go

insert into OhSnap.dbo.Users (UserLogin, UserPassword, Email, FirstName, LastName, RoleID, CreationDate) values ('MadMax', HASHBYTES('SHA2_512', 'Password'), 'max@gmail.com', 'Max', 'Mad', 1, getdate());
insert into OhSnap.dbo.Users (UserLogin, UserPassword, Email, FirstName, LastName, RoleID, CreationDate) values ('Ivan2000', HASHBYTES('SHA2_512', 'Password'), 'ivan@gmail.com', 'Ivan', 'Ivanov', 2, getdate());
insert into OhSnap.dbo.Users (UserLogin, UserPassword, Email, FirstName, LastName, RoleID, CreationDate) values ('DarkStalker', HASHBYTES('SHA2_512', 'Password'), 'darkstalker@gmail.com', 'Alex', 'Dark', 2, getdate());
insert into OhSnap.dbo.Users (UserLogin, UserPassword, Email, FirstName, LastName, RoleID, CreationDate) values ('Singer', HASHBYTES('SHA2_512', 'Password'), 'bob1941@gmail.com', 'Bob', 'Dylan', 2, getdate());
insert into OhSnap.dbo.Users (UserLogin, UserPassword, Email, FirstName, LastName, RoleID, CreationDate) values ('President', HASHBYTES('SHA2_512', 'Password'), 'bestpresident@gmail.com', 'George', 'Washington', 2, getdate());

go

CREATE TABLE OhSnap.dbo.Photos (
	PhotoID int IDENTITY(1,1) PRIMARY KEY,
	PhotoName nvarchar(50) UNIQUE NOT NULL,
	ByteArray varbinary(max) NOT NULL,
	FileType nvarchar(50) NOT NULL,
    UserID int FOREIGN KEY REFERENCES OhSnap.dbo.Users(UserID) NOT NULL,
	UploadDate datetime,
	Likes nvarchar(max),
	LikesCount int
);