#nullable disable

namespace API
{
    public class Coming
    {
        public int Id { get; set; } = 0 ;
        public int UserId { get; set; }
        public DateTime ComingDate { get; set; } = DateTime.UtcNow;
    }
}
