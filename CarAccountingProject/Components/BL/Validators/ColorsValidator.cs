namespace BL
{
    public class ColorsValidator
    {
        public static void ValidateColor(Color color)
        {
            if (color == null ||
                String.IsNullOrEmpty(color.Name))
            {
                throw new ColorsValidatorFailException();
            }
        }
    }
}