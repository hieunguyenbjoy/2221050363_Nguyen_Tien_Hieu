using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    public class ImportDetail
    {
        [Key]
        public int Id { get; set; }

        public int ImportReceiptId { get; set; }
        public int EquipmentId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
        // Thành tiền = Số lượng * Đơn giá
        public decimal LineTotal { get; set; }

        [ForeignKey("ImportReceiptId")]
        public ImportReceipt? ImportReceipt { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment? Equipment { get; set; }
    }
}