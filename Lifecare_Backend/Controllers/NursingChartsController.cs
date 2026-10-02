using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lifecare_Backend.Data;
using Lifecare_Backend.Models;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursingChartsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NursingChartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<NursingChartDto>>> GetNursingChartsForPatient(int patientId)
        {
            var charts = await _context.NursingCharts
                .Include(c => c.Medications)
                .Where(c => c.PatientId == patientId)
                .OrderByDescending(c => c.Date)
                .ToListAsync();

            var dtos = charts.Select(c => new NursingChartDto
            {
                Id = c.Id,
                PatientId = c.PatientId,
                Date = c.Date.ToString("yyyy-MM-dd"),
                VitalsIntervalHours = c.VitalsIntervalHours,
                Notes = c.Notes,
                Investigations = c.Investigations,
                Medications = c.Medications.Select(m => new NursingChartMedicationDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    IsSyrup = m.IsSyrup,
                    Morning = m.Morning,
                    Afternoon = m.Afternoon,
                    Evening = m.Evening,
                    Night = m.Night
                }).ToList()
            });

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<NursingChartDto>> SaveNursingChart([FromBody] NursingChartDto dto)
        {
            if (!DateTime.TryParse(dto.Date, out DateTime parsedDate))
            {
                return BadRequest("Invalid date format");
            }

            // Check if chart for this date already exists
            var existingChart = await _context.NursingCharts
                .Include(c => c.Medications)
                .FirstOrDefaultAsync(c => c.PatientId == dto.PatientId && c.Date.Date == parsedDate.Date);

            if (existingChart != null)
            {
                // Update existing chart
                existingChart.VitalsIntervalHours = dto.VitalsIntervalHours;
                existingChart.Notes = dto.Notes;
                existingChart.Investigations = dto.Investigations;

                // Remove old medications
                _context.NursingChartMedications.RemoveRange(existingChart.Medications);

                // Add new medications
                foreach (var m in dto.Medications)
                {
                    existingChart.Medications.Add(new NursingChartMedication
                    {
                        Name = m.Name,
                        IsSyrup = m.IsSyrup,
                        Morning = m.Morning,
                        Afternoon = m.Afternoon,
                        Evening = m.Evening,
                        Night = m.Night
                    });
                }

                await _context.SaveChangesAsync();
                return Ok(dto);
            }

            // Create new chart
            var newChart = new NursingChart
            {
                PatientId = dto.PatientId,
                Date = parsedDate.Date,
                VitalsIntervalHours = dto.VitalsIntervalHours,
                Notes = dto.Notes,
                Investigations = dto.Investigations,
                Medications = dto.Medications.Select(m => new NursingChartMedication
                {
                    Name = m.Name,
                    IsSyrup = m.IsSyrup,
                    Morning = m.Morning,
                    Afternoon = m.Afternoon,
                    Evening = m.Evening,
                    Night = m.Night
                }).ToList()
            };

            _context.NursingCharts.Add(newChart);
            await _context.SaveChangesAsync();
            
            dto.Id = newChart.Id;
            return CreatedAtAction(nameof(GetNursingChartsForPatient), new { patientId = newChart.PatientId }, dto);
        }
    }
}
