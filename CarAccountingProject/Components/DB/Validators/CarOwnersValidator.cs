namespace DB
{
    public class CarOwnersValidator
    {
        public static void ValidateCarOwner(CarOwner carOwner)
        {
            if (carOwner == null ||
                carOwner.Name.Length == 0 || 
                carOwner.Surname.Length == 0 ||
                carOwner.Email.Length == 0)
            {
                throw new CarOwnersValidatorFailException();
            }
        }
    }
}