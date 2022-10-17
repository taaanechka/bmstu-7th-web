CREATE TABLE "Users" (
    "Id" serial primary key,
    "Name" varchar not null,
    "Surname" varchar not null,
    "Login" varchar not null,
    "Password" varchar not null,
    "UserType" int not null
);
COPY "Users"("Name","Surname","Login","Password","UserType") FROM '/docker-entrypoint-initdb.d/users.csv' DELIMITER ';';
CREATE UNIQUE INDEX logins_index 
ON "Users"("Login");

CREATE TABLE "Comings" (
    "Id" serial primary key,
    "UserId" int not null,
    "ComingDate" date not null,
    foreign key ("UserId") references "Users"("Id") on delete cascade
);
COPY "Comings"("UserId","ComingDate") FROM '/docker-entrypoint-initdb.d/comings.csv' DELIMITER ';';

CREATE TABLE "Departures" (
    "Id" serial primary key,
    "UserId" int not null,
    "DepartureDate" date not null,
    foreign key ("UserId") references "Users"("Id") on delete cascade
);
COPY "Departures"("UserId","DepartureDate") FROM '/docker-entrypoint-initdb.d/departures.csv' DELIMITER ';';

CREATE TABLE "Brands" (
    "Id" serial primary key,
    "Name" varchar not null,
    "ManufactCountry" varchar(2) not null, 
    "Wheel" varchar(5) not null
);
COPY "Brands"("Name","ManufactCountry","Wheel") FROM '/docker-entrypoint-initdb.d/brands.csv' DELIMITER ';';

CREATE TABLE "Models" (
    "Id" serial primary key,
    "BrandId" int not null,
    "Name" varchar not null,
    foreign key ("BrandId") references "Brands"("Id") on delete cascade
);
COPY "Models"("BrandId","Name") FROM '/docker-entrypoint-initdb.d/models.csv' DELIMITER ';';

CREATE TABLE "Equipments" (
    "Id" serial primary key,
    "Category" varchar not null,
    "Gear" varchar not null, 
    "RoofType" varchar not null
);
COPY "Equipments"("Category","Gear","RoofType") FROM '/docker-entrypoint-initdb.d/equipments.csv' DELIMITER ';';

CREATE TABLE "Colors" (
    "Id" serial primary key,
    "Name" varchar not null
);
COPY "Colors"("Name") FROM '/docker-entrypoint-initdb.d/colors.csv' DELIMITER ';';

CREATE TABLE "Cars" (
    "Id" varchar primary key,
    "ModelId" int not null,
    "EquipmentId" int not null,
    "ColorId" int not null,
    "ComingId" int not null,
    foreign key ("ModelId") references "Models"("Id") on delete cascade,
    foreign key ("EquipmentId") references "Equipments"("Id") on delete cascade,
    foreign key ("ColorId") references "Colors"("Id") on delete cascade,
    foreign key ("ComingId") references "Comings"("Id") on delete cascade
);
COPY "Cars" FROM '/docker-entrypoint-initdb.d/cars.csv' DELIMITER ';';

CREATE TABLE "CarOwners" (
    "Id" serial primary key,
    "Name" varchar not null,
    "Surname" varchar not null,
    "Email" varchar not null
);
COPY "CarOwners"("Name","Surname","Email") FROM '/docker-entrypoint-initdb.d/car_owners.csv' DELIMITER ';';

CREATE TABLE "LinksOwnerCarDeparture" (
    "Id" serial primary key,
    "OwnerId" int not null,
    "CarId" varchar not null,
    "DepartureId" int not null,
    foreign key ("OwnerId") references "CarOwners"("Id") on delete cascade,
    foreign key ("CarId") references "Cars"("Id") on delete cascade,
    foreign key ("DepartureId") references "Departures"("Id") on delete cascade
);
COPY "LinksOwnerCarDeparture"("OwnerId","CarId","DepartureId") FROM '/docker-entrypoint-initdb.d/links_owner_car_departure.csv' DELIMITER ';';

CREATE TABLE "VIPCarOwners" (
    "Id" serial primary key,
    "CarOwnerId" int not null,
    foreign key ("CarOwnerId") references "CarOwners"("Id") on delete cascade
);
COPY "VIPCarOwners"("CarOwnerId") FROM '/docker-entrypoint-initdb.d/vipcar_owners.csv' DELIMITER ';';
