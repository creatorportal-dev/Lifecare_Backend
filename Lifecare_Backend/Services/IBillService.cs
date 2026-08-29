using Lifecare_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lifecare_Backend.Services
{
    public interface IBillService
    {
        Task<IEnumerable<BillDto>> GetAllAsync();
        Task<BillDto?> GetByIdAsync(int id);
        Task<BillDto> CreateAsync(CreateBillDto dto);
    }
}
