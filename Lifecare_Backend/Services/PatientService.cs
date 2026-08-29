using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lifecare_Backend.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        private PatientDto MapToDto(Patient p)
        {
            return new PatientDto
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Phone = p.Phone,
                Age = p.Age,
                Gender = p.Gender,
                Weight = p.Weight,
                Height = p.Height,
                Caste = p.Caste,
                AddressLine = p.AddressLine,
                State = p.State,
                City = p.City,
                Pincode = p.Pincode,
                Type = p.Type,
                Department = p.Department,
                Doctor = p.Doctor,
                OpdCharge = p.OpdCharge,
                RegisteredAt = p.RegisteredAt.ToString("o"), // ISO 8601
                Status = p.Status,
                Allergy = p.Allergy,
                Deformity = p.Deformity,
                Complaint = p.Complaint,
                Mediclaim = p.Mediclaim,
                InsuranceCompany = p.InsuranceCompany,
                PolicyNumber = p.PolicyNumber,
                PastOperations = p.PastOperations.Select(o => new PastOperationDto
                {
                    Id = o.Id,
                    Type = o.Type,
                    BodyPart = o.BodyPart,
                    Place = o.Place,
                    Deformity = o.Deformity
                }).ToList()
            };
        }

        public async Task<IEnumerable<PatientDto>> GetAllAsync()
        {
            var patients = await _context.Patients
                .Include(p => p.PastOperations)
                .OrderByDescending(p => p.RegisteredAt)
                .ToListAsync();

            return patients.Select(MapToDto);
        }

        public async Task<PatientDto?> GetByIdAsync(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.PastOperations)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null) return null;
            return MapToDto(patient);
        }

        public async Task<PatientDto?> GetByCodeAsync(string code)
        {
            var p = await _context.Patients
                .Include(x => x.PastOperations)
                .FirstOrDefaultAsync(x => x.Code == code);
            
            if (p == null) return null;
            return MapToDto(p);
        }

        public async Task<IEnumerable<PatientDto>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<PatientDto>();

            var lowerQuery = query.ToLower();
            var patients = await _context.Patients
                .Include(x => x.PastOperations)
                .Where(x => x.Code.ToLower().Contains(lowerQuery) || x.Name.ToLower().Contains(lowerQuery))
                .Take(10) // Limit results
                .ToListAsync();

            return patients.Select(MapToDto);
        }

        public async Task<PatientDto> CreateAsync(CreatePatientDto dto)
        {
            // Generate sequential code like P-2026-00001
            var year = DateTime.UtcNow.Year;
            var maxCode = await _context.Patients
                .Where(p => p.Code.StartsWith($"P-{year}-"))
                .MaxAsync(p => (string?)p.Code);

            int seq = 1;
            if (maxCode != null)
            {
                var parts = maxCode.Split('-');
                if (parts.Length == 3 && int.TryParse(parts[2], out int lastSeq))
                {
                    seq = lastSeq + 1;
                }
            }

            var newCode = $"P-{year}-{seq:D5}";

            var patient = new Patient
            {
                Code = newCode,
                Name = dto.Name,
                Phone = dto.Phone,
                Age = dto.Age,
                Gender = dto.Gender,
                Weight = dto.Weight,
                Height = dto.Height,
                Caste = dto.Caste,
                AddressLine = dto.AddressLine,
                State = dto.State,
                City = dto.City,
                Pincode = dto.Pincode,
                Type = dto.Type,
                Department = dto.Department,
                Doctor = dto.Doctor,
                OpdCharge = dto.OpdCharge,
                Status = "Waiting",
                RegisteredAt = DateTime.UtcNow,
                Allergy = dto.Allergy,
                Deformity = dto.Deformity,
                Complaint = dto.Complaint,
                Mediclaim = dto.Mediclaim,
                InsuranceCompany = dto.InsuranceCompany,
                PolicyNumber = dto.PolicyNumber,
                PastOperations = dto.PastOperations?.Select(o => new PastOperation
                {
                    Type = o.Type,
                    BodyPart = o.BodyPart,
                    Place = o.Place,
                    Deformity = o.Deformity
                }).ToList() ?? new List<PastOperation>()
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return MapToDto(patient);
        }

        public async Task<PatientDto?> UpdateStatusAsync(int id, string status)
        {
            var patient = await _context.Patients
                .Include(p => p.PastOperations)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null) return null;

            patient.Status = status;
            await _context.SaveChangesAsync();

            return MapToDto(patient);
        }
    }
}
