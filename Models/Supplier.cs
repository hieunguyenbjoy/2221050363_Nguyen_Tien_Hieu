using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(300)]
        public string? Address { get; set; }

        public string? Email { get; set; }

        // Navigation property
        public ICollection<Equipment>? Equipments { get; set; }
    }
}