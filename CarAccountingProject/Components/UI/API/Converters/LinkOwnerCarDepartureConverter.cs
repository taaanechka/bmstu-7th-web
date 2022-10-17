using BL;

namespace API
{
    public class LinkOwnerCarDepartureConverter
    {
        public static BL.LinkOwnerCarDeparture APIToBL(API.LinkOwnerCarDeparture LinkOwnerCarDepartureAPI)
        {
            return new BL.LinkOwnerCarDeparture(0, 
                                            LinkOwnerCarDepartureAPI.OwnerId, 
                                            LinkOwnerCarDepartureAPI.CarId, 
                                            0);
        }

        public static API.LinkOwnerCarDeparture BLToAPI(BL.LinkOwnerCarDeparture LinkOwnerCarDepartureBL)
        {
            API.LinkOwnerCarDeparture LinkOwnerCarDeparture= new API.LinkOwnerCarDeparture {
                OwnerId = LinkOwnerCarDepartureBL.OwnerId,
                CarId = LinkOwnerCarDepartureBL.CarId
            };

            return LinkOwnerCarDeparture;
        }
    }
}