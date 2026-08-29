using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lifecare_Backend.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        [StringLength(50)]
        public string PatientCode { get; set; } = string.Empty;

        [Required]
        public decimal Subtotal { get; set; }

        [Required]
        [StringLength(20)]
        public string DiscountType { get; set; } = "flat";

        [Required]
        public decimal DiscountValue { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;

        public ICollection<BillItem> Items { get; set; } = new List<BillItem>();
    }

    public class BillItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BillId { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Units { get; set; }

        [Required]
        public int Pieces { get; set; }

        [Required]
        public decimal Mrp { get; set; }

        [Required]
        public decimal Total { get; set; }

        [ForeignKey("BillId")]
        public Bill Bill { get; set; } = null!;
    }

    public class CreateBillDto
    {
        [Required]
        public string PatientId { get; set; } = string.Empty;

        [Required]
        public string PatientCode { get; set; } = string.Empty;

        [Required]
        public decimal Subtotal { get; set; }

        [Required]
        public string DiscountType { get; set; } = "flat";

        [Required]
        public decimal DiscountValue { get; set; }

        [Required]
        public decimal Total { get; set; }

        public List<CreateBillItemDto> Items { get; set; } = new();
    }

    public class CreateBillItemDto
    {
        [Required]
        public string MedicineId { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Units { get; set; }

        [Required]
        public int Pieces { get; set; }

        [Required]
        public decimal Mrp { get; set; }

        [Required]
        public decimal Total { get; set; }
    }

    public class BillDto
    {
        public int Id { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string PatientCode { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public string DiscountType { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public decimal Total { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public List<BillItemDto> Items { get; set; } = new();
    }

    public class BillItemDto
    {
        public string MedicineId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Units { get; set; }
        public int Pieces { get; set; }
        public decimal Mrp { get; set; }
        public decimal Total { get; set; }
    }
}
