using BL;

namespace API
{
    public class DepartureConverter
    {
        public static BL.Departure APIToBL(API.Departure DepartureAPI)
        {
            return new BL.Departure(DepartureAPI.Id, DepartureAPI.UserId, DepartureAPI.DepartureDate);
        }

        public static API.Departure BLToAPI(BL.Departure DepartureBL)
        {
            API.Departure departure = new API.Departure {
                Id = DepartureBL.Id,
                UserId = DepartureBL.UserId,
                DepartureDate = DepartureBL.DepartureDate
            };

            return departure;
        }
    }
}