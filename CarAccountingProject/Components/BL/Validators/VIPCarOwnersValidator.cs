namespace BL
{
    public class VIPCarOwnersValidator
    {
        public static void ValidateVIPCarOwner(VIPCarOwner vipOwner)
        {
            if (vipOwner == null ||
                vipOwner.CarOwnerId < 1)
            {
                throw new VIPCarOwnersValidatorFailException();
            }
        }
    }
}