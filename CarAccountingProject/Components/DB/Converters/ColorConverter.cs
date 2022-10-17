using BL;

namespace DB
{
    public class ColorConverter
    {
        public static BL.Color DBToBL(DB.Color ColorDB)
        {
            return new BL.Color(ColorDB.Id, ColorDB.Name);
        }

        public static DB.Color BLToDB(BL.Color ColorBL)
        {
            DB.Color color = new DB.Color();
            color.Name = ColorBL.Name;

            return color;
        }
    }
}