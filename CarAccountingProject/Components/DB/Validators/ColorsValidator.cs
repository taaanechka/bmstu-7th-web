namespace DB
{
    public class ColorsValidator
    {
        public static void ValidateColor(Color color)
        {
            if (color == null ||
                color.Name.Length == 0)
            {
                throw new ColorsValidatorFailException();
            }
        }
    }
}