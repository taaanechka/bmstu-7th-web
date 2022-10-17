using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DB
{
    public class Coming
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ComingDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
