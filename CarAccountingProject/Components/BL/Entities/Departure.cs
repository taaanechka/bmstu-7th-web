#nullable disable

namespace BL
{
    public class Departure
    {
        public Departure (int id, int userId, DateTime depTime)
        {
            Id = id;
            UserId = userId;
            DepartureDate = depTime;
        }

        public Departure (int id, int userId)
        {
            Id = id;
            UserId = userId;
        }

        public int Id { get; }
        public int UserId { get; }
        public DateTime DepartureDate { get; } = DateTime.UtcNow;

        public virtual User User { get; }
    }
}