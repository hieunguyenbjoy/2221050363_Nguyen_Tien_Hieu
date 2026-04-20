using System.ComponentModel.DataAnnotations;
namespace DemoMVC.Models{
public class Order
{
    public int OrderId { get; set; } // khóa chính

    public DateTime OrderDate { get; set; } // ngày đặt hàng

    public int CustomerId { get; set; } // khóa ngoại
    public Customer Customer { get; set; } 
    // mỗi đơn hàng thuộc về 1 khách hàng

    public List<OrderDetail> OrderDetails { get; set; } 
    // 1 đơn hàng có nhiều sản phẩm (thông qua bảng trung gian)
}
}