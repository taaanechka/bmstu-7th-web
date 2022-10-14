using BL;

namespace DB
{
    public class CarConverter
    {
        public static BL.Car DBToBL(DB.Car CarDB)
        {
            return new BL.Car(CarDB.Id, CarDB.ModelId, CarDB.EquipmentId, CarDB.ColorId, CarDB.ComingId);
        }

        public static DB.Car BLToDB(BL.Car CarBL)
        {
            DB.Car car = new DB.Car();
            car.Id = CarBL.Id;
            car.ModelId = CarBL.ModelId;
            car.EquipmentId = CarBL.EquipmentId;
            car.ColorId = CarBL.ColorId;
            car.ComingId = CarBL.ComingId;

            return car;
        }
    }
}