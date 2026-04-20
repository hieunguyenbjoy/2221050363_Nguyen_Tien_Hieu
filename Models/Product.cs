using System.ComponentModel.DataAnnotations;
namespace DemoMVC.Models{
public class Product
{
    public int ProductId { get; set; } // khóa chính

    [Required]
    public string ProductName { get; set; } // tên sản phẩm

    [Range(0, double.MaxValue)] // giá phải >= 0
    public decimal Price { get; set; } // giá sản phẩm

    public List<OrderDetail> OrderDetails { get; set; } 
    // 1 sản phẩm có thể nằm trong nhiều chi tiết đơn hàng
}}