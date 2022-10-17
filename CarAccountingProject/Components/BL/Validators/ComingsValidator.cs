namespace BL
{
    public class ComingsValidator
    {
        public static void ValidateComing(Coming coming)
        {
            if (coming == null ||
                coming.UserId < 1 ||
                coming.ComingDate == default(DateTime))
            // if (coming == null)
            {
                throw new ComingsValidatorFailException();
            }
        }
    }
}