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

        public async Task<PagedResult<MedicineDto>> GetAllAsync(int page = 1, int pageSize = 50)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 1000);

            var query = _context.Medicines.AsNoTracking().OrderBy(m => m.Name);
            var total = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MedicineDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    CategoryId = m.CategoryId,
                    Batch = m.Batch,
                    MfgDate = m.MfgDate.ToString("yyyy-MM-dd"),
                    ExpDate = m.ExpDate.ToString("yyyy-MM-dd"),
                    Quantity = m.Quantity,
                    Mrp = m.Mrp
                })
                .ToListAsync();

            return new PagedResult<MedicineDto> { Items = items, Page = page, PageSize = pageSize, TotalCount = total };
        }

        public async Task<PagedResult<MedicineDto>> SearchAsync(string query, int page = 1, int pageSize = 20)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new PagedResult<MedicineDto> { Items = new List<MedicineDto>(), Page = page, PageSize = pageSize, TotalCount = 0 };

            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 1000);

            var pattern = $"%{query}%";
            var dbQuery = _context.Medicines
                .AsNoTracking()
                .Where(m => EF.Functions.Like(m.Name, pattern) || EF.Functions.Like(m.Batch, pattern))
                .OrderBy(m => m.Name);

            var total = await dbQuery.CountAsync();
            var items = await dbQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MedicineDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    CategoryId = m.CategoryId,
                    Batch = m.Batch,
                    MfgDate = m.MfgDate.ToString("yyyy-MM-dd"),
                    ExpDate = m.ExpDate.ToString("yyyy-MM-dd"),
                    Quantity = m.Quantity,
                    Mrp = m.Mrp
                })
                .ToListAsync();

            return new PagedResult<MedicineDto> { Items = items, Page = page, PageSize = pageSize, TotalCount = total };
        }
        public async Task<MedicineDto?> GetByIdAsync(int id)
        {
            var m = await _context.Medicines
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new MedicineDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryId = x.CategoryId,
                    Batch = x.Batch,
                    MfgDate = x.MfgDate.ToString("yyyy-MM-dd"),
                    ExpDate = x.ExpDate.ToString("yyyy-MM-dd"),
                    Quantity = x.Quantity,
                    Mrp = x.Mrp
                })
                .FirstOrDefaultAsync();

            return m;
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
