using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(50, ErrorMessage = "ID tối đa 50 ký tự")]
        
        public string Name { get; set; }
        [Required(ErrorMessage = "Tuổi không được để trống")]
        [Range(18, 60, ErrorMessage = "Tuổi phải từ 18 đến 60")]
        
        public int Age { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        
        public string Address { get; set; }

        public int FacultyId { get; set; }

        public Faculty? Faculty { get; set; }
    }
}