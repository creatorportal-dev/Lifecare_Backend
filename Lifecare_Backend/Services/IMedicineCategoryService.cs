using Lifecare_Backend.Models;

namespace Lifecare_Backend.Services
{
    public interface IMedicineCategoryService
    {
        Task<IEnumerable<MedicineCategoryDto>> GetAllAsync();
        Task<MedicineCategoryDto?> GetByIdAsync(int id);
        Task<MedicineCategoryDto> CreateAsync(CreateMedicineCategoryDto dto);
        Task<MedicineCategoryDto?> UpdateAsync(int id, UpdateMedicineCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
