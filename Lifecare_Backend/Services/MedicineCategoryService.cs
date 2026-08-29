using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Lifecare_Backend.Services
{
    public class MedicineCategoryService : IMedicineCategoryService
    {
        private readonly ApplicationDbContext _context;

        public MedicineCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicineCategoryDto>> GetAllAsync()
        {
            return await _context.MedicineCategories
                .Select(m => new MedicineCategoryDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Unit = m.Unit,
                    PiecesPerUnit = m.PiecesPerUnit
                })
                .ToListAsync();
        }

        public async Task<MedicineCategoryDto?> GetByIdAsync(int id)
        {
            var category = await _context.MedicineCategories.FindAsync(id);
            if (category == null) return null;

            return new MedicineCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Unit = category.Unit,
                PiecesPerUnit = category.PiecesPerUnit
            };
        }

        public async Task<MedicineCategoryDto> CreateAsync(CreateMedicineCategoryDto dto)
        {
            var category = new MedicineCategory
            {
                Name = dto.Name,
                Unit = dto.Unit,
                PiecesPerUnit = dto.PiecesPerUnit
            };

            _context.MedicineCategories.Add(category);
            await _context.SaveChangesAsync();

            return new MedicineCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Unit = category.Unit,
                PiecesPerUnit = category.PiecesPerUnit
            };
        }

        public async Task<MedicineCategoryDto?> UpdateAsync(int id, UpdateMedicineCategoryDto dto)
        {
            var category = await _context.MedicineCategories.FindAsync(id);
            if (category == null) return null;

            if (dto.Name != null) category.Name = dto.Name;
            if (dto.Unit != null) category.Unit = dto.Unit;
            if (dto.PiecesPerUnit != null) category.PiecesPerUnit = dto.PiecesPerUnit.Value;

            await _context.SaveChangesAsync();

            return new MedicineCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Unit = category.Unit,
                PiecesPerUnit = category.PiecesPerUnit
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.MedicineCategories.FindAsync(id);
            if (category == null) return false;

            _context.MedicineCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
