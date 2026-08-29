using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Lifecare_Backend.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly ApplicationDbContext _context;

        public MedicineService(ApplicationDbContext context)
        {
            _context = context;
        }
        private MedicineDto MapToDto(Medicine m)
        {
            return new MedicineDto
            {
                Id = m.Id,
                Name = m.Name,
                CategoryId = m.CategoryId,
                Batch = m.Batch,
                MfgDate = m.MfgDate.ToString("yyyy-MM-dd"),
                ExpDate = m.ExpDate.ToString("yyyy-MM-dd"),
                Quantity = m.Quantity,
                Mrp = m.Mrp
            };
        }

        public async Task<IEnumerable<MedicineDto>> GetAllAsync()
        {
            var medicines = await _context.Medicines.ToListAsync();
            return medicines.Select(MapToDto);
        }

        public async Task<IEnumerable<MedicineDto>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<MedicineDto>();

            var lowerQuery = query.ToLower();
            var medicines = await _context.Medicines
                .Where(m => m.Name.ToLower().Contains(lowerQuery) || m.Batch.ToLower().Contains(lowerQuery))
                .Take(20)
                .ToListAsync();

            return medicines.Select(MapToDto);
        }
        public async Task<MedicineDto?> GetByIdAsync(int id)
        {
            var m = await _context.Medicines.FindAsync(id);
            if (m == null) return null;

            return new MedicineDto
            {
                Id = m.Id,
                Name = m.Name,
                CategoryId = m.CategoryId,
                Batch = m.Batch,
                MfgDate = m.MfgDate.ToString("yyyy-MM-dd"),
                ExpDate = m.ExpDate.ToString("yyyy-MM-dd"),
                Quantity = m.Quantity,
                Mrp = m.Mrp
            };
        }

        public async Task<MedicineDto> CreateAsync(CreateMedicineDto dto)
        {
            var medicine = new Medicine
            {
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                Batch = dto.Batch ?? string.Empty,
                MfgDate = DateTime.Parse(dto.MfgDate),
                ExpDate = DateTime.Parse(dto.ExpDate),
                Quantity = dto.Quantity,
                Mrp = dto.Mrp
            };

            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();

            return new MedicineDto
            {
                Id = medicine.Id,
                Name = medicine.Name,
                CategoryId = medicine.CategoryId,
                Batch = medicine.Batch,
                MfgDate = medicine.MfgDate.ToString("yyyy-MM-dd"),
                ExpDate = medicine.ExpDate.ToString("yyyy-MM-dd"),
                Quantity = medicine.Quantity,
                Mrp = medicine.Mrp
            };
        }

        public async Task<MedicineDto?> UpdateAsync(int id, UpdateMedicineDto dto)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null) return null;

            if (dto.Name != null) medicine.Name = dto.Name;
            if (dto.CategoryId != null) medicine.CategoryId = dto.CategoryId.Value;
            if (dto.Batch != null) medicine.Batch = dto.Batch;
            if (dto.MfgDate != null) medicine.MfgDate = DateTime.Parse(dto.MfgDate);
            if (dto.ExpDate != null) medicine.ExpDate = DateTime.Parse(dto.ExpDate);
            if (dto.Quantity != null) medicine.Quantity = dto.Quantity.Value;
            if (dto.Mrp != null) medicine.Mrp = dto.Mrp.Value;

            await _context.SaveChangesAsync();

            return new MedicineDto
            {
                Id = medicine.Id,
                Name = medicine.Name,
                CategoryId = medicine.CategoryId,
                Batch = medicine.Batch,
                MfgDate = medicine.MfgDate.ToString("yyyy-MM-dd"),
                ExpDate = medicine.ExpDate.ToString("yyyy-MM-dd"),
                Quantity = medicine.Quantity,
                Mrp = medicine.Mrp
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null) return false;

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
