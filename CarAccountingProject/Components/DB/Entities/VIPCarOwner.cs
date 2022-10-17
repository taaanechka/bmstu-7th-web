using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB
{
    public class VIPCarOwner
    {
        public int Id { get; set; }
        public int CarOwnerId { get; set; }
    }
}
