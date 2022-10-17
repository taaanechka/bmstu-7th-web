using BL;

namespace DB
{
    public class ComingConverter
    {
        public static BL.Coming DBToBL(DB.Coming ComingDB)
        {
            return new BL.Coming(ComingDB.Id, ComingDB.UserId, ComingDB.ComingDate);
        }

        public static DB.Coming BLToDB(BL.Coming ComingBL)
        {
            // DB.Coming coming = new DB.Coming();
            // coming.UserId = ComingBL.UserId;
            // coming.ComingDate = ComingBL.ComingDate;
            DB.Coming coming = new DB.Coming{
                UserId = ComingBL.UserId,
                ComingDate = ComingBL.ComingDate};

            return coming;
        }
    }
}