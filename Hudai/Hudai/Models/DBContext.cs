using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hudai.Models
{
    public class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }
        public DbSet<Products> products { get; set; }
    }
}