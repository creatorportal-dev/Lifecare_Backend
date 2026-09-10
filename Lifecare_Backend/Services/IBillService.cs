using Lifecare_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lifecare_Backend.Services
{
    public interface IBillService
    {
        Task<PagedResult<BillDto>> GetAllAsync(int page = 1, int pageSize = 50);
        Task<BillDto?> GetByIdAsync(int id);
        Task<BillDto> CreateAsync(CreateBillDto dto);
    }
}
