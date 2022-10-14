--CREATE EXTENSION IF NOT EXISTS plpython3u;

CREATE OR REPLACE FUNCTION fn_delete_departure()
RETURNS TRIGGER
AS $$
BEGIN
    IF (TG_OP = 'DELETE') THEN
        DELETE FROM "Departures"
        WHERE "Id" =  OLD."DepartureId";
        IF NOT FOUND THEN RETURN NULL; END IF;
    END IF;
    RETURN OLD;
END;
$$ LANGUAGE PLPGSQL;

CREATE TRIGGER tr_delete_departure
AFTER DELETE ON "LinksOwnerCarDeparture"
    FOR EACH ROW EXECUTE FUNCTION fn_delete_departure();
