using Lifecare_Backend.Models;

namespace Lifecare_Backend.Services
{
    public interface IMedicineService
    {
        Task<IEnumerable<MedicineDto>> GetAllAsync();
        Task<IEnumerable<MedicineDto>> SearchAsync(string query);
        Task<MedicineDto?> GetByIdAsync(int id);
        Task<MedicineDto> CreateAsync(CreateMedicineDto dto);
        Task<MedicineDto?> UpdateAsync(int id, UpdateMedicineDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
