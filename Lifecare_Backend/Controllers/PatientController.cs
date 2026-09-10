using Lifecare_Backend.Models;
using Lifecare_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<PatientDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var patients = await _patientService.GetAllAsync(page, pageSize);
            return Ok(patients);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PatientDto>> Get(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        [HttpGet("code/{code}")]
        public async Task<ActionResult<PatientDto>> GetByCode(string code)
        {
            var p = await _patientService.GetByCodeAsync(code);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<PatientDto>>> Search([FromQuery] string q, [FromQuery] bool prescribedOnly = false)
        {
            var patients = await _patientService.SearchAsync(q, prescribedOnly);
            return Ok(patients);
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create(CreatePatientDto dto)
        {
            var patient = await _patientService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = patient.Id }, patient);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<PatientDto>> UpdateStatus(int id, [FromBody] UpdatePatientStatusDto dto)
        {
            var patient = await _patientService.UpdateStatusAsync(id, dto.Status);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _patientService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
