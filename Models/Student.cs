using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Student
    {
        [Key]
        public string StudentCode { get; set; } = default!;

        public string FullName { get; set; } = default!;
    }
}