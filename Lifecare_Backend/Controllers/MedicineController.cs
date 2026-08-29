using Lifecare_Backend.Models;
using Lifecare_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> GetAll()
        {
            var medicines = await _medicineService.GetAllAsync();
            return Ok(medicines);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> Search([FromQuery] string q)
        {
            var medicines = await _medicineService.SearchAsync(q);
            return Ok(medicines);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MedicineDto>> Get(int id)
        {
            var medicine = await _medicineService.GetByIdAsync(id);
            if (medicine == null) return NotFound();
            return Ok(medicine);
        }

        [HttpPost]
        public async Task<ActionResult<MedicineDto>> Create(CreateMedicineDto dto)
        {
            var medicine = await _medicineService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = medicine.Id }, medicine);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicineDto>> Update(int id, UpdateMedicineDto dto)
        {
            var medicine = await _medicineService.UpdateAsync(id, dto);
            if (medicine == null) return NotFound();
            return Ok(medicine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _medicineService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
