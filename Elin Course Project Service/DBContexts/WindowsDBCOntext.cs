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
        public WindowsDBContext(DbContextOptions<WindowsDBContext> options): base(options){ }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Positions>()
                .HasKey(c => c.PositionID);
            modelBuilder.Entity<Staff>()
                .HasKey(c => c.Passport);
            modelBuilder.Entity<Departments>()
                .HasKey(c => c.DepartmentID);
        }
    }
}
