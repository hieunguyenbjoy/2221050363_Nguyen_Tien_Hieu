using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Controllers
{
    public class ExportReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: Hiển thị form tạo phiếu xuất
        public IActionResult Create()
        {
            ViewBag.Equipments = new SelectList(_context.Equipments, "Id", "Name");
            return View(new ExportViewModel());
        }

        // 2. POST: Xử lý lưu phiếu xuất và TRỪ TỒN KHO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExportViewModel model)
        {
            if (ModelState.IsValid)
            {
                // NHIỆM VỤ 1: Kiểm tra trước xem có đủ hàng trong kho không (Pre-check)
                if (model.Details != null && model.Details.Count > 0)
                {
                    foreach (var item in model.Details)
                    {
                        var eqCheck = _context.Equipments.AsNoTracking().FirstOrDefault(e => e.Id == item.EquipmentId);
                        if (eqCheck == null || eqCheck.StockQuantity < item.Quantity)
                        {
                            // Báo lỗi ngay lập tức về màn hình nếu kho không đủ hàng
                            ModelState.AddModelError("", $"Thiết bị '{eqCheck?.Name}' không đủ hàng. Tồn kho hiện tại chỉ còn: {eqCheck?.StockQuantity ?? 0}");
                            ViewBag.Equipments = new SelectList(_context.Equipments, "Id", "Name");
                            return View(model);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Vui lòng thêm ít nhất 1 thiết bị để xuất kho.");
                    ViewBag.Equipments = new SelectList(_context.Equipments, "Id", "Name");
                    return View(model);
                }

                // NHIỆM VỤ 2: Tạo Phiếu Xuất (Master)
                var receipt = new ExportReceipt
                {
                    ReceiptDate = model.ReceiptDate,
                    Note = model.Note,
                    TotalAmount = 0
                };
                _context.ExportReceipts.Add(receipt);

                decimal totalAmount = 0;

                // NHIỆM VỤ 3: Lưu chi tiết & Trừ tồn kho
                foreach (var item in model.Details)
                {
                    var lineTotal = item.Quantity * item.UnitPrice;
                    totalAmount += lineTotal;

                    var detail = new ExportDetail
                    {
                        ExportReceipt = receipt,
                        EquipmentId = item.EquipmentId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        LineTotal = lineTotal
                    };
                    _context.ExportDetails.Add(detail);

                    // 👉 TRỪ TỒN KHO 
                    var equipment = _context.Equipments.Find(item.EquipmentId);
                    if (equipment != null)
                    {
                        equipment.StockQuantity -= item.Quantity; // Phép TRỪ (-)
                        _context.Equipments.Update(equipment);
                    }
                }

                receipt.TotalAmount = totalAmount;
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Equipments = new SelectList(_context.Equipments, "Id", "Name");
            return View(model);
        }

        // 3. GET: Hiển thị danh sách phiếu xuất
        public IActionResult Index()
        {
            var receipts = _context.ExportReceipts.OrderByDescending(r => r.ReceiptDate).ToList();
            return View(receipts);
        }
    }
}