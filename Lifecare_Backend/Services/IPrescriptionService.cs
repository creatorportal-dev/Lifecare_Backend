using Lifecare_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lifecare_Backend.Services
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<PrescriptionDto>> GetAllAsync();
        Task<PrescriptionDto?> GetByIdAsync(int id);
        Task<PrescriptionDto> CreateAsync(CreatePrescriptionDto dto);
    }
}
