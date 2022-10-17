#nullable disable

namespace BL
{
    public class VIPCarOwner
    {
        public VIPCarOwner (int id, int carOwnerId)
        {
            Id = id;
            CarOwnerId = carOwnerId;
        }

        public int Id { get; }
        public int CarOwnerId { get; }

        public virtual CarOwner CarOwner { get; }
    }
}
