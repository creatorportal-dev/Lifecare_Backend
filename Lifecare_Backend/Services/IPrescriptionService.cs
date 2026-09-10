using Lifecare_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lifecare_Backend.Services
{
    public interface IPrescriptionService
    {
        Task<PagedResult<PrescriptionDto>> GetAllAsync(int page = 1, int pageSize = 50);
        Task<PrescriptionDto?> GetByIdAsync(int id);
        Task<PrescriptionDto> CreateAsync(CreatePrescriptionDto dto);
    }
}
