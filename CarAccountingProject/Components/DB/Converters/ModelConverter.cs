using BL;

namespace DB
{
    public class ModelConverter
    {
        public static BL.Model DBToBL(DB.Model ModelDB)
        {
            return new BL.Model(ModelDB.Id, ModelDB.BrandId, ModelDB.Name);
        }

        public static DB.Model BLToDB(BL.Model ModelBL)
        {
            DB.Model model = new DB.Model();
            model.BrandId = ModelBL.BrandId;
            model.Name = ModelBL.Name;

            return model;
        }
    }
}