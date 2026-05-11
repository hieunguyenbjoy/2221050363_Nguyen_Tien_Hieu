using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    public class ImportReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: Hiển thị giao diện tạo phiếu nhập (chưa lưu gì cả)
        public IActionResult Create()
        {
            // Truyền danh sách Thiết bị sang giao diện để làm cái Dropdown (thẻ <select>) cho người dùng chọn
            ViewBag.Equipments = new SelectList(_context.Equipments, "Id", "Name");
            
            // Ném cái khuôn trống (ViewModel) sang cho View điền vào
            return View(new ImportViewModel()); 
        }

        // 2. POST: Nơi nhận dữ liệu từ màn hình và xử lý 3 nhiệm vụ cùng lúc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ImportViewModel model)
        {
            if (ModelState.IsValid)
            {
                // NHIỆM VỤ 1: Tạo Phiếu Nhập (Master)
                var receipt = new ImportReceipt
                {
                    ReceiptDate = model.ReceiptDate,
                    Note = model.Note,
                    TotalAmount = 0 // Tạm thời để 0, lát tính tổng xong sẽ cập nhật lại
                };

                // Đưa phiếu nhập vào hàng chờ chuẩn bị lưu
                _context.ImportReceipts.Add(receipt);

                decimal totalAmount = 0;

                // Kiểm tra xem người dùng có nhập danh sách thiết bị nào không
                if (model.Details != null && model.Details.Count > 0)
                {
                    // Lặp qua từng dòng thiết bị người dùng gửi lên
                    foreach (var item in model.Details)
                    {
                        // Tính thành tiền của dòng này
                        var lineTotal = item.Quantity * item.UnitPrice;
                        totalAmount += lineTotal;

                        // NHIỆM VỤ 2: Tạo Chi tiết phiếu nhập (Detail)
                        var detail = new ImportDetail
                        {
                            ImportReceipt = receipt, // tự động móc nối với Phiếu Nhập ở trên
                            EquipmentId = item.EquipmentId,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            LineTotal = lineTotal
                        };
                        _context.ImportDetails.Add(detail); // Đưa chi tiết vào hàng chờ

                        // NHIỆM VỤ 3: TÌM THIẾT BỊ VÀ CỘNG TỒN KHO
                        var equipment = _context.Equipments.Find(item.EquipmentId);
                        if (equipment != null)
                        {
                            equipment.StockQuantity += item.Quantity; // Cộng dồn số lượng mới nhập vào kho
                            _context.Equipments.Update(equipment); // Cập nhật lại kho
                        }
                    }
                }

                // Cập nhật lại tổng tiền cuối cùng cho toàn bộ phiếu nhập
                receipt.TotalAmount = totalAmount;

                // CHỐT HẠ: Lưu TẤT CẢ vào Database trong 1 lần duy nhất!
                // Nghĩa là nếu việc cộng tồn kho bị lỗi, nó sẽ tự hủy luôn việc tạo phiếu nhập để bảo toàn dữ liệu.
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            // Nếu dữ liệu form bị lỗi, load lại danh sách thiết bị và trả form về bắt nhập lại
            ViewBag.Equipments = new SelectList(_context.Equipments, "Id", "Name");
            return View(model);
        }

        // 3. GET: Giao diện xem danh sách các Phiếu Nhập (đã lưu)
        public IActionResult Index()
        {
            var receipts = _context.ImportReceipts
                .OrderByDescending(r => r.ReceiptDate)
                .ToList();
            return View(receipts);
        }
        // GET: ImportReceipt/Details/5
public IActionResult Details(int? id)
{
    if (id == null) return NotFound();

    // Lấy thông tin phiếu nhập kèm theo danh sách chi tiết và tên thiết bị
    var receipt = _context.ImportReceipts
        .Include(r => r.ImportDetails!)
            .ThenInclude(d => d.Equipment)
        .FirstOrDefault(m => m.Id == id);

    if (receipt == null) return NotFound();

    return View(receipt);
}
// POST: ImportReceipt/Delete/5
public IActionResult Delete(int id)
{
    var receipt = _context.ImportReceipts
        .Include(r => r.ImportDetails)
        .FirstOrDefault(r => r.Id == id);

    if (receipt != null)
    {
        // Xóa các chi tiết trước để tránh lỗi khóa ngoại
        if (receipt.ImportDetails != null)
        {
            _context.ImportDetails.RemoveRange(receipt.ImportDetails);
        }
        _context.ImportReceipts.Remove(receipt);
        _context.SaveChanges();
    }
    return RedirectToAction(nameof(Index));
}
    }
}