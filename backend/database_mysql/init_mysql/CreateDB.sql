USE car-accounting-mysql;

CREATE TABLE Users (
    Id int primary key auto_increment,
    Name varchar(50) not null,
    Surname varchar(50) not null,
    Login varchar(50) not null,
    Password varchar(50) not null,
    UserType int not null
);
LOAD DATA INFILE "/var/lib/mysql-files/users.csv" 
INTO TABLE Users
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n'
(Name, Surname, Login, Password, UserType);


CREATE TABLE Comings (
    Id int primary key auto_increment,
    UserId int not null,
    ComingDate date not null,
    foreign key (UserId) references Users(Id) on delete cascade
);
LOAD DATA INFILE "/var/lib/mysql-files/comings.csv" 
INTO TABLE Comings
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n'
(UserId, ComingDate);


CREATE TABLE Departures (
    Id int primary key auto_increment,
    UserId int not null,
    DepartureDate date not null,
    foreign key (UserId) references Users(Id) on delete cascade
);
LOAD DATA INFILE "/var/lib/mysql-files/departures.csv" 
INTO TABLE Departures
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n'
(UserId, DepartureDate);


CREATE TABLE Brands (
    Id int primary key auto_increment,
    Name varchar(50) not null,
    ManufactCountry varchar(2) not null, 
    Wheel varchar(5) not null

);
LOAD DATA INFILE "/var/lib/mysql-files/brands.csv" 
INTO TABLE Brands
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n'
(Name, ManufactCountry, Wheel);


CREATE TABLE Models (
    Id int primary key auto_increment,
    BrandId int not null,
    Name varchar(50) not null,
    foreign key (BrandId) references Brands(Id) on delete cascade
);
LOAD DATA INFILE "/var/lib/mysql-files/models.csv" 
INTO TABLE Models
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n'
(BrandId, Name);


CREATE TABLE Equipments (
    Id int primary key auto_increment,
    Category varchar(50) not null,
    Gear varchar(50) not null, 
    RoofType varchar(50) not null
);
LOAD DATA INFILE "/var/lib/mysql-files/equipments.csv" 
INTO TABLE Equipments
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n'
(Category, Gear, RoofType);


CREATE TABLE Colors (
    Id int primary key auto_increment,
    Name varchar(50) not null
);
LOAD DATA INFILE "/var/lib/mysql-files/colors.csv" 
INTO TABLE Colors
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n'
(Name);


CREATE TABLE Cars (
    Id varchar(20) primary key,
    ModelId int not null,
    EquipmentId int not null,
    ColorId int not null,
    ComingId int not null,
    foreign key (ModelId) references Models(Id) on delete cascade,
    foreign key (EquipmentId) references Equipments(Id) on delete cascade,
    foreign key (ColorId) references Colors(Id) on delete cascade,
    foreign key (ComingId) references Comings(Id) on delete cascade
);
LOAD DATA INFILE "/var/lib/mysql-files/cars.csv" 
INTO TABLE Cars
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n';


CREATE TABLE CarOwners (
    Id int primary key auto_increment,
    Name varchar(50) not null,
    Surname varchar(50) not null,
    Email varchar(100) not null
);
LOAD DATA INFILE "/var/lib/mysql-files/car_owners.csv" 
INTO TABLE CarOwners
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n'
(Name, Surname, Email);


CREATE TABLE LinksOwnerCarDeparture (
    Id int primary key auto_increment,
    OwnerId int not null,
    CarId varchar(20) not null,
    DepartureId int not null,
    foreign key (OwnerId) references CarOwners(Id) on delete cascade,
    foreign key (CarId) references Cars(Id) on delete cascade,
    foreign key (DepartureId) references Departures(Id) on delete cascade
);
LOAD DATA INFILE "/var/lib/mysql-files/links_owner_car_departure.csv" 
INTO TABLE LinksOwnerCarDeparture
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n'
(OwnerId, CarId, DepartureId);


CREATE TABLE VIPCarOwners (
    Id int primary key auto_increment,
    CarOwnerId int not null,
    foreign key (CarOwnerId) references CarOwners(Id) on delete cascade
);
LOAD DATA INFILE "/var/lib/mysql-files/vipcar_owners.csv" 
INTO TABLE VIPCarOwners
COLUMNS TERMINATED BY ';'
OPTIONALLY ENCLOSED BY '"' ESCAPED BY '"' LINES
TERMINATED BY '\n'
(CarOwnerId);
