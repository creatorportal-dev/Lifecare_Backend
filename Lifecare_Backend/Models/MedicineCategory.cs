using System.ComponentModel.DataAnnotations;

namespace Lifecare_Backend.Models
{
    public class MedicineCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public int PiecesPerUnit { get; set; }
    }

    public class CreateMedicineCategoryDto
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public int PiecesPerUnit { get; set; }
    }

    public class UpdateMedicineCategoryDto
    {
        [StringLength(250)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Unit { get; set; }

        public int? PiecesPerUnit { get; set; }
    }

    public class MedicineCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int PiecesPerUnit { get; set; }
    }
}
