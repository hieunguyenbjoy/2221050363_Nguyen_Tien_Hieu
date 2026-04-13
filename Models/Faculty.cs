using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }

        [Required(ErrorMessage = "Tên khoa không được để trống")]
        public string FacultyName { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
    }
}