using Microsoft.EntityFrameworkCore;
using CPS;

namespace Elin_Course_Project_Service.DBContexts
{
    /// <summary>
    ///  Класс для работы с БД
    /// </summary>
    public class WindowsDBContext : DbContext
    {
        public DbSet<Models.StaffModel> Staff { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Models.CustomersModel> Customers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Models.OrderModel> Orders { get; set; }
        public DbSet<Models.ProductsToOrders> ProductsToOrders {get;set;}
        public DbSet<Models.CustomerInfo> CustomerInfo { get; set; }

        /// <summary>
        ///  Конфигуратор подключения к БД
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fenrir\Source\Repos\dthfenrisulfr\ELINCPS\Elin Course Project Service\WindowsDataBase.mdf;Integrated Security=True", providerOptions => providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        /// <summary>
        ///  Создаёт модель БД на языке C#
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Positions>()
                .HasKey(x => x.PositionID);
            modelBuilder.Entity<Models.CustomerInfo>()
                .HasKey(x => x.CustomerID);
            modelBuilder.Entity<Models.StaffModel>()
                .HasKey(c => c.Passport);
            modelBuilder.Entity<Departments>()
                .HasKey(c => c.DepartmentID);
            modelBuilder.Entity<Models.CustomersModel>()
                .HasKey(x => x.CustomerID);
            modelBuilder.Entity<Models.OrderModel>()
                .HasKey(x => x.OrderID);
            modelBuilder.Entity<Products>()
                .HasKey(x => x.ProductID);
            modelBuilder.Entity<Models.ProductsToOrders>()
               .HasKey(x => x.ProductID);
        }
    }
}
