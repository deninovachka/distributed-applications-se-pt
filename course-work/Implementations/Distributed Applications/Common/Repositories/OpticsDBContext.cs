using Microsoft.EntityFrameworkCore;
using OpticsWebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repositories
{
    public class OpticsDBContext : DbContext
    {
        public OpticsDBContext(DbContextOptions<OpticsDBContext> options)
            : base(options) 
        {

        }
        

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Product> Products { get; set; }


    }
}
