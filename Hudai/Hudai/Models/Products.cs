using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hudai.Models
{
    public class Products
    {
        public Guid ID { get; set; }

        public String Name { get; set; }

        public String Category { get; set; }

        public Int16 Price { get; set; }
    }
}