using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DB
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Gear { get; set; }
        public string RoofType { get; set; }
    }
}
