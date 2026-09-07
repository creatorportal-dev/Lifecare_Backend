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
                Ward = p.Ward,
                WardNumber = p.WardNumber,
                RelativeName = p.RelativeName,
                Relation = p.Relation,
                RelativePhone = p.RelativePhone,
                RelativeAddress = p.RelativeAddress,
                MaritalStatus = p.MaritalStatus,
                Child = p.Child,
                Occupation = p.Occupation,
                Religion = p.Religion,
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
            // Use paging by default to avoid returning huge result sets and AsNoTracking for read-only
            return await _context.Patients
                .AsNoTracking()
                .OrderByDescending(p => p.RegisteredAt)
                .Take(100)
                .Select(p => new PatientDto
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
                    Ward = p.Ward,
                    WardNumber = p.WardNumber,
                    RelativeName = p.RelativeName,
                    Relation = p.Relation,
                    RelativePhone = p.RelativePhone,
                    RelativeAddress = p.RelativeAddress,
                    MaritalStatus = p.MaritalStatus,
                    Child = p.Child,
                    Occupation = p.Occupation,
                    Religion = p.Religion,
                    PastOperations = p.PastOperations.Select(o => new PastOperationDto
                    {
                        Id = o.Id,
                        Type = o.Type,
                        BodyPart = o.BodyPart,
                        Place = o.Place,
                        Deformity = o.Deformity
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<PatientDto?> GetByIdAsync(int id)
        {
            var patient = await _context.Patients
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PatientDto
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
                    Ward = p.Ward,
                    WardNumber = p.WardNumber,
                    RelativeName = p.RelativeName,
                    Relation = p.Relation,
                    RelativePhone = p.RelativePhone,
                    RelativeAddress = p.RelativeAddress,
                    MaritalStatus = p.MaritalStatus,
                    Child = p.Child,
                    Occupation = p.Occupation,
                    Religion = p.Religion,
                    PastOperations = p.PastOperations.Select(o => new PastOperationDto
                    {
                        Id = o.Id,
                        Type = o.Type,
                        BodyPart = o.BodyPart,
                        Place = o.Place,
                        Deformity = o.Deformity
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return patient;
        }

        public async Task<PatientDto?> GetByCodeAsync(string code)
        {
            var p = await _context.Patients
                .AsNoTracking()
                .Where(x => x.Code == code)
                .Select(x => new PatientDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Phone = x.Phone,
                    Age = x.Age,
                    Gender = x.Gender,
                    RegisteredAt = x.RegisteredAt.ToString("o"),
                    PastOperations = x.PastOperations.Select(o => new PastOperationDto
                    {
                        Id = o.Id,
                        Type = o.Type,
                        BodyPart = o.BodyPart,
                        Place = o.Place,
                        Deformity = o.Deformity
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return p;
        }

        public async Task<IEnumerable<PatientDto>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<PatientDto>();

            var pattern = $"%{query}%";
            return await _context.Patients
                .AsNoTracking()
                .Where(x => EF.Functions.Like(x.Code, pattern) || EF.Functions.Like(x.Name, pattern))
                .OrderByDescending(x => x.RegisteredAt)
                .Take(10)
                .Select(x => new PatientDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Phone = x.Phone,
                    RegisteredAt = x.RegisteredAt.ToString("o")
                })
                .ToListAsync();
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
                Ward = dto.Ward,
                WardNumber = dto.WardNumber,
                RelativeName = dto.RelativeName,
                Relation = dto.Relation,
                RelativePhone = dto.RelativePhone,
                RelativeAddress = dto.RelativeAddress,
                MaritalStatus = dto.MaritalStatus,
                Child = dto.Child,
                Occupation = dto.Occupation,
                Religion = dto.Religion,
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

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return false;

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
