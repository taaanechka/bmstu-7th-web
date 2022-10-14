--ROLES
CREATE role employee with LOGIN ENCRYPTED PASSWORD 'password';
CREATE role analyst with LOGIN ENCRYPTED PASSWORD 'password';
CREATE role admin with LOGIN ENCRYPTED PASSWORD 'password';

-- PERMISSIONS
GRANT select, delete ON "Comings", "Departures" TO admin;
GRANT ALL ON "Users", "Colors", "Cars", "Models", "Brands", "Equipments", "LinksOwnerCarDeparture", "CarOwners" TO admin;

GRANT select, insert ON "Comings", "Departures" TO employee;
GRANT select, insert, delete ON "Cars", "LinksOwnerCarDeparture" TO employee;
GRANT select, update ON "Users" TO employee;

GRANT select ON "Comings", "Departures", "Users" TO analyst;

GRANT USAGE, SELECT ON SEQUENCE "Comings_Id_seq" TO employee;
GRANT USAGE, SELECT ON SEQUENCE "Departures_Id_seq" TO employee;
GRANT USAGE, SELECT ON SEQUENCE "Users_Id_seq" TO employee;

GRANT USAGE, SELECT ON SEQUENCE "Comings_Id_seq" TO analyst;
GRANT USAGE, SELECT ON SEQUENCE "Departures_Id_seq" TO analyst;
GRANT USAGE, SELECT ON SEQUENCE "Users_Id_seq" TO analyst;

GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO admin;
