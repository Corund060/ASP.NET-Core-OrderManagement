using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() : base("Data Source = localhost;Database=OrderManagement;Integrated security = SSPI") { }       

        public DbSet<Item> Items { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Basket> Baskets { get; set; }
    }
}
