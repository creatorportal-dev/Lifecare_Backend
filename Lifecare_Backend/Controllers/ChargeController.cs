using Lifecare_Backend.Models;
using Lifecare_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargeController : ControllerBase
    {
        private readonly IChargeService _chargeService;

        public ChargeController(IChargeService chargeService)
        {
            _chargeService = chargeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChargeDto>>> GetAll()
        {
            var charges = await _chargeService.GetAllAsync();
            return Ok(charges);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChargeDto>> Get(int id)
        {
            var charge = await _chargeService.GetByIdAsync(id);
            if (charge == null) return NotFound();
            return Ok(charge);
        }

        [HttpPost]
        public async Task<ActionResult<ChargeDto>> Create(CreateChargeDto dto)
        {
            var charge = await _chargeService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = charge.Id }, charge);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ChargeDto>> Update(int id, UpdateChargeDto dto)
        {
            var charge = await _chargeService.UpdateAsync(id, dto);
            if (charge == null) return NotFound();
            return Ok(charge);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _chargeService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
