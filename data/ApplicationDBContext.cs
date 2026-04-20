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
        public DbSet<Customer> Customers { get; set; } // bảng khách hàng
        public DbSet<Product> Products { get; set; } // bảng sản phẩm
        public DbSet<Order> Orders { get; set; } // bảng đơn hàng
        public DbSet<OrderDetail> OrderDetails { get; set; } // bảng chi tiết đơn hàng
    }
}