using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lifecare_Backend.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionService(ApplicationDbContext context)
        {
            _context = context;
        }

        private PrescriptionDto MapToDto(Prescription p)
        {
            return new PrescriptionDto
            {
                Id = p.Id,
                PatientId = p.Patient.Id.ToString(), // Map to string for frontend
                Diagnosis = p.Diagnosis,
                Disease = p.Disease,
                Suggestion = p.Suggestion,
                FollowUpDate = p.FollowUpDate,
                CourseDays = p.CourseDays,
                CreatedAt = p.CreatedAt.ToString("o"),
                Medicines = p.Medicines.Select(m => new PrescribedMedicineDto
                {
                    MedicineId = m.MedicineId.ToString(),
                    Name = m.Name,
                    Morning = m.Morning,
                    Afternoon = m.Afternoon,
                    Evening = m.Evening,
                    Night = m.Night
                }).ToList()
            };
        }

        public async Task<IEnumerable<PrescriptionDto>> GetAllAsync()
        {
            var prescriptions = await _context.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Medicines)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return prescriptions.Select(MapToDto);
        }

        public async Task<PrescriptionDto?> GetByIdAsync(int id)
        {
            var p = await _context.Prescriptions
                .Include(x => x.Patient)
                .Include(x => x.Medicines)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (p == null) return null;
            return MapToDto(p);
        }

        public async Task<PrescriptionDto> CreateAsync(CreatePrescriptionDto dto)
        {
            var patientId = int.Parse(dto.PatientId); // Assuming frontend passes the numeric ID as string

            var prescription = new Prescription
            {
                PatientId = patientId,
                Diagnosis = dto.Diagnosis,
                Disease = dto.Disease,
                Suggestion = dto.Suggestion,
                FollowUpDate = dto.FollowUpDate,
                CourseDays = dto.CourseDays,
                CreatedAt = DateTime.UtcNow,
                Medicines = dto.Medicines.Select(m => new PrescribedMedicine
                {
                    MedicineId = int.Parse(m.MedicineId),
                    Name = m.Name,
                    Morning = m.Morning,
                    Afternoon = m.Afternoon,
                    Evening = m.Evening,
                    Night = m.Night
                }).ToList()
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            // Load related patient for mapping
            await _context.Entry(prescription).Reference(p => p.Patient).LoadAsync();

            return MapToDto(prescription);
        }
    }
}
