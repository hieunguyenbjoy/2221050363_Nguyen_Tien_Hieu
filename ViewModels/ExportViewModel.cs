using System.ComponentModel.DataAnnotations;

namespace DemoMVC.ViewModels
{
    public class ExportViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập ngày")]
        public DateTime ReceiptDate { get; set; } = DateTime.Now;

        public string? Note { get; set; }

        public List<ExportDetailViewModel> Details { get; set; } = new List<ExportDetailViewModel>();
    }

    public class ExportDetailViewModel
    {
        public int EquipmentId { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Đơn giá không hợp lệ")]
        public decimal UnitPrice { get; set; }
    }
}