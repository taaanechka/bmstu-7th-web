using BL;

namespace DB
{
    public class EquipmentConverter
    {
        public static BL.Equipment DBToBL(DB.Equipment EquipmentDB)
        {
            return new BL.Equipment(EquipmentDB.Id, EquipmentDB.Category, EquipmentDB.Gear, EquipmentDB.RoofType);
        }

        public static DB.Equipment BLToDB(BL.Equipment EquipmentBL)
        {
            DB.Equipment equipment = new DB.Equipment();
            equipment.Category = EquipmentBL.Category;
            equipment.Gear = EquipmentBL.Gear;
            equipment.RoofType = EquipmentBL.RoofType;

            return equipment;
        }
    }
}