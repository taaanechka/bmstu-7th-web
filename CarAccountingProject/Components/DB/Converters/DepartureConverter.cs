using BL;

namespace DB
{
    public class DepartureConverter
    {
        public static BL.Departure DBToBL(DB.Departure DepartureDB)
        {
            return new BL.Departure(DepartureDB.Id, DepartureDB.UserId, DepartureDB.DepartureDate);
        }

        public static DB.Departure BLToDB(BL.Departure DepartureBL)
        {
            DB.Departure departure = new DB.Departure();
            departure.UserId = DepartureBL.UserId;
            departure.DepartureDate = DepartureBL.DepartureDate;

            return departure;
        }
    }
}