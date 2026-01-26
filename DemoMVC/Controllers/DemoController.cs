using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Hello Nguyễn Tiến Hiếu - MSSV: 2221050363";
            return View();
        }
    }
}
