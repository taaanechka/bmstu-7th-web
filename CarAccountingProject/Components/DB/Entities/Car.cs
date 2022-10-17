using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using BL;

#nullable disable

namespace DB
{
    public class Car
    {
        public string Id { get; set; }
        public int ModelId { get; set; }
        public int EquipmentId { get; set; }
        public int ColorId { get; set; }
        public int ComingId { get; set; }

        // [ForeignKey("ModelId")]
        public virtual Model Model { get; set; }

        // [ForeignKey("EquipmentId")]
        public virtual Equipment Equipment { get; set; }

        // [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }

        // [ForeignKey("ComingId")]
        public virtual Coming Coming { get; set; }
    }
}