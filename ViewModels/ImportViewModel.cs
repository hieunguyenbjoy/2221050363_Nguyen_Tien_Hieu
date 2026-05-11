using System.ComponentModel.DataAnnotations;

namespace DemoMVC.ViewModels
{
    // Class này dùng để hứng toàn bộ dữ liệu từ màn hình Nhập kho gửi về
    public class ImportViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập ngày")]
        public DateTime ReceiptDate { get; set; } = DateTime.Now;

        public string? Note { get; set; }

        // Một phiếu nhập sẽ chứa một danh sách (List) các chi tiết thiết bị
        public List<ImportDetailViewModel> Details { get; set; } = new List<ImportDetailViewModel>();
    }

    // Class này đại diện cho 1 dòng thiết bị được thêm vào trong phiếu nhập
    public class ImportDetailViewModel
    {
        public int EquipmentId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá không hợp lệ")]
        public decimal UnitPrice { get; set; }
    }
}