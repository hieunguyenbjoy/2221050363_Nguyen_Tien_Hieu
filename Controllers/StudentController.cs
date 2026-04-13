using DemoMVC.Models;
using DemoMVC.Data;
using DemoMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            ViewBag.Faculties = new SelectList(_context.Faculties, "FacultyId", "FacultyName");
            return View();
        }

        // Lưu student mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DemoMVC.Models.Student student)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Faculties = new SelectList(_context.Faculties, "FacultyId", "FacultyName");

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
        public IActionResult Edit(int id, DemoMVC.Models.Student student)
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
        public IActionResult Index()
{
            var studentsWithFaculty = _context.Students
                .Include(s => s.Faculty) // liên kết đến Faculty
                .Select(s => new StudentFacultyViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    FacultyName = s.Faculty.FacultyName
                })
                .ToList();

            return View(studentsWithFaculty);
        }
    }
}