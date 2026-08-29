using Lifecare_Backend.Models;

namespace Lifecare_Backend.Services
{
    public interface IChargeService
    {
        Task<IEnumerable<ChargeDto>> GetAllAsync();
        Task<ChargeDto?> GetByIdAsync(int id);
        Task<ChargeDto> CreateAsync(CreateChargeDto dto);
        Task<ChargeDto?> UpdateAsync(int id, UpdateChargeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
