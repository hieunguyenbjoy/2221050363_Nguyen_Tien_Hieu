using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Customer
    {
        public int CustomerId { get; set; } // khóa chính

        [Required]
        public string CustomerName { get; set; } // tên khách hàng

        public List<Order> Orders { get; set; } // 1 khách hàng có nhiều đơn hàng
    }
}