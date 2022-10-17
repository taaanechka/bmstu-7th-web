using BL;

namespace DB
{
    public class BrandConverter
    {
        public static BL.Brand DBToBL(DB.Brand BrandDB)
        {
            return new BL.Brand(BrandDB.Id, BrandDB.Name, BrandDB.ManufactCountry, BrandDB.Wheel);
        }

        public static DB.Brand BLToDB(BL.Brand BrandBL)
        {
            DB.Brand brand = new DB.Brand();
            brand.Name = BrandBL.Name;
            brand.ManufactCountry = BrandBL.ManufactCountry;
            brand.Wheel = BrandBL.Wheel;

            return brand;
        }
    }
}