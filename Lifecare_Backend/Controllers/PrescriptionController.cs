using Lifecare_Backend.Models;
using Lifecare_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<PrescriptionDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var prescriptions = await _prescriptionService.GetAllAsync(page, pageSize);
            return Ok(prescriptions);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PrescriptionDto>> Get(int id)
        {
            var p = await _prescriptionService.GetByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public async Task<ActionResult<PrescriptionDto>> Create(CreatePrescriptionDto dto)
        {
            var p = await _prescriptionService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
        }
    }
}
