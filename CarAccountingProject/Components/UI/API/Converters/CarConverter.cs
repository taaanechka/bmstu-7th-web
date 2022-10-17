using BL;

namespace API
{
    public class CarConverter
    {
        public static BL.Car APIToBL(API.Car CarAPI)
        {
            return new BL.Car(CarAPI.Id, CarAPI.ModelId, CarAPI.EquipmentId, CarAPI.ColorId, 0);
        }

        public static API.Car BLToAPI(BL.Car CarBL)
        {
            API.Car car = new API.Car {
                Id = CarBL.Id,
                ModelId = CarBL.ModelId,
                EquipmentId = CarBL.EquipmentId,
                ColorId = CarBL.ColorId
            };

            return car;
        }
    }
}