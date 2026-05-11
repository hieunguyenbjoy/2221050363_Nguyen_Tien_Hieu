using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên loại thiết bị không được để trống")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        // Navigation property
        public ICollection<Equipment>? Equipments { get; set; }
    }
}