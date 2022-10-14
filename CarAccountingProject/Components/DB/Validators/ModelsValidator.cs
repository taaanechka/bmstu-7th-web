namespace DB
{
    public class ModelsValidator
    {
        public static void ValidateModel(Model model)
        {
            if (model== null ||
                model.BrandId < 1 ||
                model.Name.Length == 0 )
            {
                throw new ModelsValidatorFailException();
            }
        }
    }
}