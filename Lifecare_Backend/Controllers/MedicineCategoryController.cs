using Lifecare_Backend.Models;
using Lifecare_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineCategoryController : ControllerBase
    {
        private readonly IMedicineCategoryService _medicineCategoryService;

        public MedicineCategoryController(IMedicineCategoryService medicineCategoryService)
        {
            _medicineCategoryService = medicineCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineCategoryDto>>> GetAll()
        {
            var categories = await _medicineCategoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineCategoryDto>> Get(int id)
        {
            var category = await _medicineCategoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<MedicineCategoryDto>> Create(CreateMedicineCategoryDto dto)
        {
            var category = await _medicineCategoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicineCategoryDto>> Update(int id, UpdateMedicineCategoryDto dto)
        {
            var category = await _medicineCategoryService.UpdateAsync(id, dto);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _medicineCategoryService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
