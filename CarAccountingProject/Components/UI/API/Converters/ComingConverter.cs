using BL;

namespace API
{
    public class ComingConverter
    {
        public static BL.Coming APIToBL(API.Coming ComingAPI)
        {
            return new BL.Coming(ComingAPI.Id, ComingAPI.UserId, ComingAPI.ComingDate);
        }

        public static API.Coming BLToAPI(BL.Coming ComingBL)
        {
            API.Coming coming = new API.Coming {
                Id = ComingBL.Id,
                UserId = ComingBL.UserId,
                ComingDate = ComingBL.ComingDate
            };

            return coming;
        }
    }
}