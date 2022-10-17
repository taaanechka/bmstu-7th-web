namespace BL
{
    public class CarOwnersValidator
    {
        public static void ValidateCarOwner(CarOwner carOwner)
        {
            if (carOwner == null ||
                String.IsNullOrEmpty(carOwner.Name) || 
                String.IsNullOrEmpty(carOwner.Surname) ||
                String.IsNullOrEmpty(carOwner.Email))
            {
                throw new CarOwnersValidatorFailException();
            }
        }
    }
}