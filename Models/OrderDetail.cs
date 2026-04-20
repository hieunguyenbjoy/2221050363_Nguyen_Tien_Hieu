using System.ComponentModel.DataAnnotations;
namespace DemoMVC.Models{
public class OrderDetail
{
    public int OrderDetailId { get; set; } // khóa chính

    public int OrderId { get; set; } // FK tới Order
    public Order Order { get; set; } // liên kết tới bảng Order

    public int ProductId { get; set; } // FK tới Product
    public Product Product { get; set; } // liên kết tới bảng Product

    [Range(1, 1000)] // số lượng phải >= 1
    public int Quantity { get; set; } // số lượng sản phẩm trong đơn
}}