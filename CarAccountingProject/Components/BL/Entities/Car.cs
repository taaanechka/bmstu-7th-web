#nullable disable

namespace BL
{
    public class Car
    {
        public Car (string id, int modelId, int equipmentId, int colorId, int comingId)
        {
            Id = id;
            ModelId = modelId;
            EquipmentId = equipmentId;
            ColorId = colorId;
            ComingId = comingId;
        }

        public string Id { get; }
        public int ModelId { get; }
        public int EquipmentId { get; }
        public int ColorId { get; }
        public int ComingId { get; }

        public virtual Model Model { get; }
        public virtual Equipment Equipment { get; }
        public virtual Color Color { get; }
        public virtual Coming Coming { get; }
    }
}