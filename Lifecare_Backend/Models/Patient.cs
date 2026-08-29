using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lifecare_Backend.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        public double? Weight { get; set; }
        public double? Height { get; set; }

        [StringLength(100)]
        public string? Caste { get; set; }

        [Required]
        public string AddressLine { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Pincode { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Department { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Doctor { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal OpdCharge { get; set; }

        [Required]
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Waiting";

        // Medical History
        public string? Allergy { get; set; }
        public string? Deformity { get; set; }
        public string? Complaint { get; set; }

        public bool? Mediclaim { get; set; }

        [StringLength(200)]
        public string? InsuranceCompany { get; set; }

        [StringLength(100)]
        public string? PolicyNumber { get; set; }

        [StringLength(100)]
        public string? Ward { get; set; }

        [StringLength(50)]
        public string? WardNumber { get; set; }

        [StringLength(100)]
        public string? RelativeName { get; set; }

        [StringLength(100)]
        public string? Relation { get; set; }

        [StringLength(20)]
        public string? RelativePhone { get; set; }

        [StringLength(250)]
        public string? RelativeAddress { get; set; }

        [StringLength(50)]
        public string? MaritalStatus { get; set; }

        [StringLength(50)]
        public string? Child { get; set; }

        [StringLength(100)]
        public string? Occupation { get; set; }

        [StringLength(100)]
        public string? Religion { get; set; }

        public ICollection<PastOperation> PastOperations { get; set; } = new List<PastOperation>();
    }

    public class PastOperation
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string BodyPart { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Place { get; set; } = string.Empty;

        public string? Deformity { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; } = null!;
    }

    public class CreatePatientDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string? Caste { get; set; }

        [Required]
        public string AddressLine { get; set; } = string.Empty;

        [Required]
        public string State { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        public string? Pincode { get; set; }

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Doctor { get; set; } = string.Empty;

        [Required]
        public decimal OpdCharge { get; set; }

        public string? Allergy { get; set; }
        public string? Deformity { get; set; }
        public string? Complaint { get; set; }

        public bool? Mediclaim { get; set; }
        public string? InsuranceCompany { get; set; }
        public string? PolicyNumber { get; set; }

        public List<CreatePastOperationDto>? PastOperations { get; set; }

        public string? Ward { get; set; }
        public string? WardNumber { get; set; }

        public string? RelativeName { get; set; }
        public string? Relation { get; set; }
        public string? RelativePhone { get; set; }
        public string? RelativeAddress { get; set; }

        public string? MaritalStatus { get; set; }
        public string? Child { get; set; }
        public string? Occupation { get; set; }
        public string? Religion { get; set; }
    }

    public class CreatePastOperationDto
    {
        public string Type { get; set; } = string.Empty;
        public string BodyPart { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public string? Deformity { get; set; }
    }

    public class PatientDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string? Caste { get; set; }
        public string AddressLine { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? Pincode { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Doctor { get; set; } = string.Empty;
        public decimal OpdCharge { get; set; }
        public string RegisteredAt { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Allergy { get; set; }
        public string? Deformity { get; set; }
        public string? Complaint { get; set; }
        public bool? Mediclaim { get; set; }
        public string? InsuranceCompany { get; set; }
        public string? PolicyNumber { get; set; }
        public List<PastOperationDto> PastOperations { get; set; } = new();

        public string? Ward { get; set; }
        public string? WardNumber { get; set; }

        public string? RelativeName { get; set; }
        public string? Relation { get; set; }
        public string? RelativePhone { get; set; }
        public string? RelativeAddress { get; set; }

        public string? MaritalStatus { get; set; }
        public string? Child { get; set; }
        public string? Occupation { get; set; }
        public string? Religion { get; set; }
    }

    public class PastOperationDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string BodyPart { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public string? Deformity { get; set; }
    }

    public class UpdatePatientStatusDto
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
