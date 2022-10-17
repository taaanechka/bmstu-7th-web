using BL;

namespace DB
{
    public class LinkOwnerCarDepartureConverter
    {
        public static BL.LinkOwnerCarDeparture DBToBL(DB.LinkOwnerCarDeparture LinkOwnerCarDepartureDB)
        {
            return new BL.LinkOwnerCarDeparture(LinkOwnerCarDepartureDB.Id, 
                                            LinkOwnerCarDepartureDB.OwnerId, 
                                            LinkOwnerCarDepartureDB.CarId, 
                                            LinkOwnerCarDepartureDB.DepartureId);
        }

        public static DB.LinkOwnerCarDeparture BLToDB(BL.LinkOwnerCarDeparture LinkOwnerCarDepartureBL)
        {
            DB.LinkOwnerCarDeparture LinkOwnerCarDeparture= new DB.LinkOwnerCarDeparture();
            LinkOwnerCarDeparture.OwnerId = LinkOwnerCarDepartureBL.OwnerId;
            LinkOwnerCarDeparture.CarId = LinkOwnerCarDepartureBL.CarId;
            LinkOwnerCarDeparture.DepartureId = LinkOwnerCarDepartureBL.DepartureId;

            return LinkOwnerCarDeparture;
        }
    }
}