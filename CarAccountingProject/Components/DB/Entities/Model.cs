using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DB
{
    public class Model
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
    }
}
