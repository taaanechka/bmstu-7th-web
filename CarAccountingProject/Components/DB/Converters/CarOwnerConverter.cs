using BL;

namespace DB
{
    public class CarOwnerConverter
    {
        public static BL.CarOwner DBToBL(DB.CarOwner CarOwnerDB)
        {
            return new BL.CarOwner(CarOwnerDB.Id, CarOwnerDB.Name, CarOwnerDB.Surname, CarOwnerDB.Email);
        }

        public static DB.CarOwner BLToDB(BL.CarOwner CarOwnerBL)
        {
            DB.CarOwner carOwner = new DB.CarOwner();
            carOwner.Name = CarOwnerBL.Name;
            carOwner.Surname = CarOwnerBL.Surname;
            carOwner.Email = CarOwnerBL.Email;

            return carOwner;
        }
    }
}