#nullable disable

namespace BL
{
    public class Model
    {
        public Model (int id, int brandId, string name)
        {
            Id = id;
            BrandId = brandId;
            Name = name;
        }

        public int Id { get; }
        public int BrandId { get; }
        public string Name { get; }

        public virtual Brand Brand { get; }
    }
}
