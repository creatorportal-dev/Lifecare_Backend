using Lifecare_Backend.Models;
using Lifecare_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lifecare_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(CreateEmployeeDto dto)
        {
            var employee = await _employeeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> Update(int id, UpdateEmployeeDto dto)
        {
            var employee = await _employeeService.UpdateAsync(id, dto);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("{id}/toggle")]
        public async Task<ActionResult<EmployeeDto>> ToggleActive(int id)
        {
            var employee = await _employeeService.ToggleActiveAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost("login")]
        public async Task<ActionResult<EmployeeDto>> Login(LoginDto dto)
        {
            var employee = await _employeeService.LoginAsync(dto);
            if (employee == null) return Unauthorized(new { message = "Invalid credentials or inactive account" });
            return Ok(employee);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // For cookie/JWT based auth, handle clearing cookies/tokens here.
            // Currently using local storage, so simply return OK.
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
