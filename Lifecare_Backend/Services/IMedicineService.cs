using Lifecare_Backend.Models;

namespace Lifecare_Backend.Services
{
    public interface IMedicineService
    {
        Task<PagedResult<MedicineDto>> GetAllAsync(int page = 1, int pageSize = 50);
        Task<PagedResult<MedicineDto>> SearchAsync(string query, int page = 1, int pageSize = 20);
        Task<MedicineDto?> GetByIdAsync(int id);
        Task<MedicineDto> CreateAsync(CreateMedicineDto dto);
        Task<MedicineDto?> UpdateAsync(int id, UpdateMedicineDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
