using Lifecare_Backend.Models;

namespace Lifecare_Backend.Services
{
    public interface IChargeService
    {
        Task<PagedResult<ChargeDto>> GetAllAsync(int page = 1, int pageSize = 50);
        Task<ChargeDto?> GetByIdAsync(int id);
        Task<ChargeDto> CreateAsync(CreateChargeDto dto);
        Task<ChargeDto?> UpdateAsync(int id, UpdateChargeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
