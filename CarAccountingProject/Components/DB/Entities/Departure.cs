using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DB
{
    public class Departure
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DepartureDate { get; set; } = DateTime.UtcNow;


        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}