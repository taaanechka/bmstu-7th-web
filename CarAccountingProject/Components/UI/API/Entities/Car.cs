#nullable disable

using System.ComponentModel.DataAnnotations;

namespace API
{
    public class Car
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public int ModelId { get; set; }

        [Required]
        public int EquipmentId { get; set; }

        [Required]
        public int ColorId { get; set; }
        // public int ComingId { get; set; }
    }
}