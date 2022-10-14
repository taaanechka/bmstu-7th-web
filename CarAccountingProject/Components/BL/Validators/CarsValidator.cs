namespace BL
{
    public class CarsValidator
    {
        public static void ValidateCar(Car car)
        {
            if (car == null ||
                car.ModelId < 1 || 
                car.EquipmentId < 1 ||
                car.ColorId < 1)
            {
                throw new CarsValidatorFailException();
            }
        }
    }
}