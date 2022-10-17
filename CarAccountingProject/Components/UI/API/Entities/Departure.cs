#nullable disable

namespace API
{
    public class Departure
    {
        public int Id { get; set; } = 0;
        public int UserId { get; set; }
        public DateTime DepartureDate { get; set; } = DateTime.UtcNow;
    }
}