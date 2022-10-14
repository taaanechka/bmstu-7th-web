#nullable disable

namespace BL
{
    public class Coming
    {
        public Coming (int id, int userId, DateTime comTime)
        {
            Id = id;
            UserId = userId;
            ComingDate = comTime;
        }

        public Coming (int id, int userId)
        {
            Id = id;
            UserId = userId;
        }

        public int Id { get; }
        public int UserId { get; }
        public DateTime ComingDate { get; } = DateTime.UtcNow;

        public virtual User User { get; }
    }
}
