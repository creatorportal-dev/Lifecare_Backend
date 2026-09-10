using Lifecare_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lifecare_Backend.Services
{
    public interface IPatientService
    {
        Task<PagedResult<PatientDto>> GetAllAsync(int page = 1, int pageSize = 50);
        Task<PatientDto?> GetByIdAsync(int id);
        Task<PatientDto?> GetByCodeAsync(string code);
        Task<PagedResult<PatientDto>> SearchAsync(string query, bool prescribedOnly = false, int page = 1, int pageSize = 10);
        Task<PatientDto> CreateAsync(CreatePatientDto dto);
        Task<PatientDto?> UpdateStatusAsync(int id, string status);
        Task<bool> DeleteAsync(int id);
    }
}
