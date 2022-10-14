namespace DB
{
    public class BrandsValidator
    {
        public static void ValidateBrand(Brand brand)
        {
            if (brand == null ||
                brand.Name.Length == 0 || 
                brand.ManufactCountry.Length == 0 ||
                brand.Wheel.Length == 0)
            {
                throw new BrandsValidatorFailException();
            }
        }
    }
}