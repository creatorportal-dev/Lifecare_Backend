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

        public async Task<PagedResult<PrescriptionDto>> GetAllAsync(int page = 1, int pageSize = 50)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 1000);

            var query = _context.Prescriptions.AsNoTracking().OrderByDescending(p => p.CreatedAt);
            var total = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PrescriptionDto
                {
                    Id = p.Id,
                    PatientId = p.PatientId.ToString(),
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
                })
                .ToListAsync();

            return new PagedResult<PrescriptionDto> { Items = items, Page = page, PageSize = pageSize, TotalCount = total };
        }

        public async Task<PrescriptionDto?> GetByIdAsync(int id)
        {
            var p = await _context.Prescriptions
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new PrescriptionDto
                {
                    Id = x.Id,
                    PatientId = x.PatientId.ToString(),
                    Diagnosis = x.Diagnosis,
                    Disease = x.Disease,
                    Suggestion = x.Suggestion,
                    FollowUpDate = x.FollowUpDate,
                    CourseDays = x.CourseDays,
                    CreatedAt = x.CreatedAt.ToString("o"),
                    Medicines = x.Medicines.Select(m => new PrescribedMedicineDto
                    {
                        MedicineId = m.MedicineId.ToString(),
                        Name = m.Name,
                        Morning = m.Morning,
                        Afternoon = m.Afternoon,
                        Evening = m.Evening,
                        Night = m.Night
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return p;
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
