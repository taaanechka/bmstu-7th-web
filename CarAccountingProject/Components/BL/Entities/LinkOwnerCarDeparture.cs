using System;
using System.Collections.Generic;

#nullable disable

namespace BL
{
    public class LinkOwnerCarDeparture
    {
        public LinkOwnerCarDeparture (int id, int ownerId, string carId, int departureId)
        {
            Id = id;
            OwnerId = ownerId;
            CarId = carId;
            DepartureId = departureId;
        }
        public int Id { get; }
        public int OwnerId { get; }
        public string CarId { get; }
        public int DepartureId { get; }

        public virtual CarOwner Owner { get; }
        public virtual Car Car { get; set; }
        public virtual Departure Departure { get; set; }
    }
}
