using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

public class StudentController : Controller
{
    // Hiển thị form
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Nhận dữ liệu từ form
    [HttpPost]
    public IActionResult Create(Student student)
    {
        ViewBag.Message = "Thêm sinh viên thành công!";
        return View(student);   // gửi lại model về view
    }
}
