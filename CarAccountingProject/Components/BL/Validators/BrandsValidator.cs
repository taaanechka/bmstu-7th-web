namespace BL
{
    public class BrandsValidator
    {
        public static void ValidateBrand(Brand brand)
        {
            if (brand == null ||
                String.IsNullOrEmpty(brand.Name) || 
                String.IsNullOrEmpty(brand.ManufactCountry) ||
                String.IsNullOrEmpty(brand.Wheel))
            {
                throw new BrandsValidatorFailException();
            }
        }
    }
}