using DemoMVC.Models;
using DemoMVC.Data;
using DemoMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using DemoMVC.Services;
using System.Linq; 
using System; // Cần thiết cho Console.WriteLine

namespace DemoMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        // 1. Khai báo thêm ExcelService
        private readonly ExcelService _excelService; 

        // 2. Truyền ExcelService vào hàm khởi tạo
        public StudentController(ApplicationDbContext context, ExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
        }

        // --- CÁC HÀM CŨ GIỮ NGUYÊN ---
        public IActionResult Create()
        {
            ViewBag.Faculties = new SelectList(_context.Faculties, "FacultyId", "FacultyName");
            return View();
        }

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

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();
            return View(student);
        }

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

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();
            return View(student);
        }

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
                .Include(s => s.Faculty)
                .Select(s => new StudentFacultyViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    FacultyName = s.Faculty.FacultyName
                })
                .ToList();

            return View(studentsWithFaculty);
        }

        // --- IMPORT EXCEL ĐÃ ĐƯỢC CẬP NHẬT ---
        public IActionResult ImportExcel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return RedirectToAction("Index");

            // Gọi máy đa năng, truyền hướng dẫn lắp ráp thành Student
            var importedStudents = _excelService.ReadExcel<Student>(file, rowData =>
            {
                // Kiểm tra nếu tên rỗng thì bỏ qua
                if (string.IsNullOrWhiteSpace(rowData[0])) return null; 

                return new Student
                {
                    Name = rowData[0],
                    Age = int.TryParse(rowData[1], out int age) ? age : 0,
                    Address = rowData[2],
                    FacultyId = int.TryParse(rowData[3], out int fId) ? fId : 0
                };
            });

            if (importedStudents == null || importedStudents.Count == 0)
                return RedirectToAction("Index");

            // Lưu vào database và check trùng
            foreach (var student in importedStudents)
            {
                bool isDuplicate = _context.Students.Any(s => s.Name.ToLower().Trim() == student.Name.ToLower().Trim());

                if (!isDuplicate)
                {
                    _context.Students.Add(student);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}