using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

#nullable disable

namespace DB
{
    public class LinkOwnerCarDeparture
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string CarId { get; set; }
        public int DepartureId { get; set; }

        public virtual CarOwner Owner { get; set; }
        public virtual Car Car { get; set; }
        public virtual Departure Departure { get; set; }
    }
}
