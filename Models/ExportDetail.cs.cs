using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    public class ExportDetail
    {
        [Key]
        public int Id { get; set; }


        public int ExportReceiptId { get; set; }
        public int EquipmentId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        
        // Thành tiền = Số lượng * Đơn giá
        public decimal LineTotal { get; set; }

        [ForeignKey("ExportReceiptId")]
        public ExportReceipt? ExportReceipt { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment? Equipment { get; set; }
    }
}