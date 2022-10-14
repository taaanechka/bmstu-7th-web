namespace DB
{
    public class EquipmentsValidator
    {
        public static void ValidateEquipment(Equipment equipment)
        {
            if (equipment == null ||
                equipment.Category.Length == 0 || 
                equipment.Gear.Length == 0 ||
                equipment.RoofType.Length == 0)
            {
                throw new EquipmentsValidatorFailException();
            }
        }
    }
}