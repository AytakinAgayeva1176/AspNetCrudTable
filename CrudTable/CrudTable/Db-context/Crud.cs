using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CrudTable.Db_context
{
    public class Crud : DbContext
    {

        public Crud()
            : base("Server = DESKTOP-5P45H04\\SQLEXPRESS; Database = Crud; Trusted_Connection = True;")
        {
        }
        public DbSet<Product> Products { get; set; }
    }

}