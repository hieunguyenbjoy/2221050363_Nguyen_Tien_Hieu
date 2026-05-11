using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class ImportReceipt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ReceiptDate { get; set; } = DateTime.Now;

        public string? Note { get; set; }

        public decimal TotalAmount { get; set; }

        // 1 phiếu nhập có nhiều chi tiết
        public ICollection<ImportDetail>? ImportDetails { get; set; }
    }
}