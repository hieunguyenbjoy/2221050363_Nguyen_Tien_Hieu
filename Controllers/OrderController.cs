using Microsoft.AspNetCore.Mvc;
using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    // DANH SÁCH ORDER
    public IActionResult Index()
    {
        var orders = _context.Orders
        .Include(o => o.Customer) // lấy bảng Customer
        .ToList();
        return View(orders);
    }

    // FORM CREATE
    public IActionResult Create()
    {
        ViewBag.Customers = new SelectList(
            _context.Customers,
            "CustomerId",
            "CustomerName"
        ); // load dropdown

        return View();
    }

    // XỬ LÝ CREATE
    [HttpPost]
    public IActionResult Create(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}