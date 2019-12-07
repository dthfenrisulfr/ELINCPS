using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Elin_Course_Project_Service.DBContexts
{
    public class WindowsDBContext : DbContext
    {
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Models.ProductsToOrders> ProductsToOrders {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Fenrir\\source\\repos\\ElinCourseProjectService\\ElinCourseProjectService\\WindowsDataBase.mdf;Integrated Security=True;Connect Timeout=30", providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Positions>()
                .HasKey(x => x.PositionID);
            modelBuilder.Entity<Staff>()
                .HasKey(c => c.Passport);
            modelBuilder.Entity<Departments>()
                .HasKey(c => c.DepartmentID);
            modelBuilder.Entity<Customers>()
                .HasKey(x => x.CustomerID);
            modelBuilder.Entity<Orders>()
                .HasKey(x => x.OrderID);
            modelBuilder.Entity<Products>()
                .HasKey(x => x.ProductID);
        }
    }
}
