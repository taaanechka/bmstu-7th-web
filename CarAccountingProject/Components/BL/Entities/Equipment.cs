namespace BL
{
    public class Equipment
    {
        public Equipment (int id, string category, string gear, string rooftype)
        {
            Id = id;
            Category = category;
            Gear = gear;
            RoofType = rooftype;
        }

        public int Id { get; }
        public string Category { get; }
        public string Gear { get; }
        public string RoofType { get; }
    }
}
