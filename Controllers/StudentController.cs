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
using System;
using System.Threading.Tasks; // Cần thiết cho async/await

namespace DemoMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelService _excelService; 

        public StudentController(ApplicationDbContext context, ExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
        }

        // 1. Khung tranh trống
        public IActionResult Index()
        {
            return View();
        }

        // 2. Lấy dữ liệu bảng (Read)
        public async Task<IActionResult> GetStudents(int page = 1, int pageSize = 10)
        {
            var query = _context.Students
                .Include(s => s.Faculty)
                .AsNoTracking()
                .OrderByDescending(x => x.Id);

            var totalItems = await query.CountAsync();

            var students = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = new PagedResult<Student>
            {
                Items = students,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return PartialView("_StudentTable", result);
        }

        // 3. Form Thêm mới (GET)
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Faculties = new SelectList(_context.Faculties, "FacultyId", "FacultyName");
            return PartialView("_Create");
        }

        // 4. Xử lý Lưu Thêm mới (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Faculties = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
                return PartialView("_Create", student);
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // 5. Form Cập nhật (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            ViewBag.Faculties = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
            return PartialView("_Edit", student);
        }

        // 6. Xử lý Lưu Cập nhật (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Faculties = new SelectList(_context.Faculties, "FacultyId", "FacultyName", student.FacultyId);
                return PartialView("_Edit", student);
            }

            var existingStudent = await _context.Students.FindAsync(student.Id);
            if (existingStudent == null) return NotFound();

            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Address = student.Address;
            existingStudent.FacultyId = student.FacultyId;

            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        // 7. Form Xác nhận Xóa (GET)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students
                .Include(s => s.Faculty)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (student == null) return NotFound();

            return PartialView("_Delete", student);
        }

        // 8. Xử lý Xóa (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Student student)
        {
            var existingStudent = await _context.Students.FindAsync(student.Id);

            if (existingStudent == null)
            {
                return Json(new { success = false });
            }

            _context.Students.Remove(existingStudent);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        public IActionResult ImportExcel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return RedirectToAction("Index");

            var importedStudents = _excelService.ReadExcel<Student>(file, rowData =>
            {
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