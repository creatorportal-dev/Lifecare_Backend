using Lifecare_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lifecare_Backend.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllAsync();
        Task<PatientDto?> GetByIdAsync(int id);
        Task<PatientDto?> GetByCodeAsync(string code);
        Task<IEnumerable<PatientDto>> SearchAsync(string query);
        Task<PatientDto> CreateAsync(CreatePatientDto dto);
        Task<PatientDto?> UpdateStatusAsync(int id, string status);
    }
}
