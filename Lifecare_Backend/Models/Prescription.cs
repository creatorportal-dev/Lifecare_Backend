using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lifecare_Backend.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        public string? Diagnosis { get; set; }
        public string? Disease { get; set; }
        public string? Suggestion { get; set; }

        public string? FollowUpDate { get; set; }

        public int? CourseDays { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;

        public ICollection<PrescribedMedicine> Medicines { get; set; } = new List<PrescribedMedicine>();
    }

    public class PrescribedMedicine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PrescriptionId { get; set; }

        [Required]
        public int MedicineId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Morning { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Afternoon { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Evening { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Night { get; set; } = string.Empty;

        [ForeignKey("PrescriptionId")]
        public Prescription Prescription { get; set; } = null!;
    }

    public class CreatePrescriptionDto
    {
        [Required]
        public string PatientId { get; set; } = string.Empty;

        public string? Diagnosis { get; set; }
        public string? Disease { get; set; }
        public string? Suggestion { get; set; }
        public string? FollowUpDate { get; set; }
        public int? CourseDays { get; set; }

        public List<CreatePrescribedMedicineDto> Medicines { get; set; } = new();
    }

    public class CreatePrescribedMedicineDto
    {
        [Required]
        public string MedicineId { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Morning { get; set; } = string.Empty;
        public string Afternoon { get; set; } = string.Empty;
        public string Evening { get; set; } = string.Empty;
        public string Night { get; set; } = string.Empty;
    }

    public class PrescriptionDto
    {
        public int Id { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
        public string? Disease { get; set; }
        public string? Suggestion { get; set; }
        public string? FollowUpDate { get; set; }
        public int? CourseDays { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public List<PrescribedMedicineDto> Medicines { get; set; } = new();
    }

    public class PrescribedMedicineDto
    {
        public string MedicineId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Morning { get; set; } = string.Empty;
        public string Afternoon { get; set; } = string.Empty;
        public string Evening { get; set; } = string.Empty;
        public string Night { get; set; } = string.Empty;
    }
}
