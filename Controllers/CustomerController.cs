using Microsoft.AspNetCore.Mvc;
using DemoMVC.Data;
using DemoMVC.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
public class CustomerController : Controller
{
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // HIỂN THỊ DANH SÁCH
    public IActionResult Index()
    {
        var customers = _context.Customers.ToList();
        return View(customers);
    }

    // HIỂN THỊ FORM CREATE
    public IActionResult Create()
    {
        return View();
    }

    // XỬ LÝ CREATE
    [HttpPost]
    public IActionResult Create(Customer customer)
    {
        Console.WriteLine("DA VAO POST");
        Console.WriteLine(customer.CustomerName);

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return RedirectToAction("Index"); // luôn quay về danh sách
    }
    public IActionResult Orders(int id)
    {
    var customer = _context.Customers
        .Include(c => c.Orders)
            .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
        .FirstOrDefault(c => c.CustomerId == id);

    return View(customer);
    }
}