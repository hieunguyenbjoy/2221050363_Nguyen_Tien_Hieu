using Microsoft.AspNetCore.Mvc;
using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class OrderDetailController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderDetailController(ApplicationDbContext context)
    {
        _context = context;
    }

    // DANH SÁCH
    public IActionResult Index()
    {
    var data = _context.OrderDetails
        .Include(od => od.Order)
            .ThenInclude(o => o.Customer)
        .Include(od => od.Product)
        .ToList();

    return View(data);
}

    // FORM CREATE
    public IActionResult Create()
    {
        ViewBag.Orders = new SelectList(
            _context.Orders,
            "OrderId",
            "OrderId"
        );

        ViewBag.Products = new SelectList(
            _context.Products,
            "ProductId",
            "ProductName"
        );

        return View();
    }

    // XỬ LÝ CREATE
    [HttpPost]
    public IActionResult Create(OrderDetail od)
    {
        _context.OrderDetails.Add(od);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}