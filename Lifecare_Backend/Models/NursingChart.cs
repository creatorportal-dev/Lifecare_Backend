using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lifecare_Backend.Models
{
    public class NursingChart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int VitalsIntervalHours { get; set; } = 2;

        public string? Notes { get; set; }
        public string? Investigations { get; set; }

        public ICollection<NursingChartMedication> Medications { get; set; } = new List<NursingChartMedication>();

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
    }

    public class NursingChartMedication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NursingChartId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public bool IsSyrup { get; set; }

        public string? Morning { get; set; }
        public string? Afternoon { get; set; }
        public string? Evening { get; set; }
        public string? Night { get; set; }

        [ForeignKey("NursingChartId")]
        public NursingChart? NursingChart { get; set; }
    }

    public class NursingChartDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Date { get; set; } = string.Empty;
        public int VitalsIntervalHours { get; set; }
        public string? Notes { get; set; }
        public string? Investigations { get; set; }
        public List<NursingChartMedicationDto> Medications { get; set; } = new List<NursingChartMedicationDto>();
    }

    public class NursingChartMedicationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSyrup { get; set; }
        public string? Morning { get; set; }
        public string? Afternoon { get; set; }
        public string? Evening { get; set; }
        public string? Night { get; set; }
    }
}
