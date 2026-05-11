using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên thiết bị")]
        [Required(ErrorMessage = "Tên thiết bị không được để trống")]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng chọn Loại thiết bị")]
        [Display(Name = "Loại thiết bị")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Nhà cung cấp")]
         [Display(Name = "Nhà Cung Cấp")]
        public int SupplierId { get; set; }

        // Giá mặc định (có thể là giá bán hoặc giá nhập tham khảo)
        public decimal Price { get; set; } 

        // Số lượng tồn kho hiện tại (Hệ thống sẽ tự cộng/trừ khi nhập/xuất)
        [Display(Name = "Số lượng tồn")]
        public int StockQuantity { get; set; }

        // Navigation properties
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
    }
}