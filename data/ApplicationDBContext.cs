using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<Product> Products { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<OrderDetail> OrderDetails { get; set; } 

        // CÁC BẢNG QUẢN LÝ KHO 
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ImportReceipt> ImportReceipts { get; set; }
        public DbSet<ImportDetail> ImportDetails { get; set; }
        public DbSet<ExportReceipt> ExportReceipts { get; set; }
        public DbSet<ExportDetail> ExportDetails { get; set; }
    }
}