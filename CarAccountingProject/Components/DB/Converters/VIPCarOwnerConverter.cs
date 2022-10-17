using BL;

namespace DB
{
    public class VIPCarOwnerConverter
    {
        public static BL.VIPCarOwner DBToBL(DB.VIPCarOwner CarOwnerDB)
        {
            return new BL.VIPCarOwner(CarOwnerDB.Id, CarOwnerDB.CarOwnerId);
        }

        public static DB.VIPCarOwner BLToDB(BL.VIPCarOwner CarOwnerBL)
        {
            DB.VIPCarOwner carOwner = new DB.VIPCarOwner();
            carOwner.CarOwnerId = CarOwnerBL.CarOwnerId;

            return carOwner;
        }
    }
}