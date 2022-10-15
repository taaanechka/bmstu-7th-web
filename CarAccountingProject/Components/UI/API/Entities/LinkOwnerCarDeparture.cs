using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace API
{
    public class LinkOwnerCarDeparture
    {
        [Required]
        public int OwnerId { get; set; }

        [Required]
        public string CarId { get; set; }
        // public int DepartureId { get; set; }
    }
}
