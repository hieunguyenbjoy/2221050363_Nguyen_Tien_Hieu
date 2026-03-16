using DemoMVC.Data;
using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        // Hiển thị form tạo mới
        public IActionResult Create()
        {
            return View();
        }

        // Lưu student mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // Hiển thị form Edit
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            return View(student);
        }

        // Lưu chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // Hiển thị xác nhận xóa
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            return View(student);
        }

        // Xóa student
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // Xem chi tiết student (tùy chọn)
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            return View(student);
        }
    }
}