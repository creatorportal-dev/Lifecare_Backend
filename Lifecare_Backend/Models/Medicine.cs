using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lifecare_Backend.Models
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [StringLength(100)]
        public string Batch { get; set; } = string.Empty;

        [Required]
        public DateTime MfgDate { get; set; }

        [Required]
        public DateTime ExpDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Mrp { get; set; }
    }

    public class CreateMedicineDto
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [StringLength(100)]
        public string Batch { get; set; } = string.Empty;

        [Required]
        public string MfgDate { get; set; } = string.Empty;

        [Required]
        public string ExpDate { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Mrp { get; set; }
    }

    public class UpdateMedicineDto
    {
        [StringLength(250)]
        public string? Name { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(100)]
        public string? Batch { get; set; }

        public string? MfgDate { get; set; }

        public string? ExpDate { get; set; }

        public int? Quantity { get; set; }

        public decimal? Mrp { get; set; }
    }

    public class MedicineDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Batch { get; set; } = string.Empty;
        public string MfgDate { get; set; } = string.Empty;
        public string ExpDate { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Mrp { get; set; }
    }
}
