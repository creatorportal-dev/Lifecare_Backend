using Lifecare_Backend.Models;
using Lifecare_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillDto>>> GetAll()
        {
            var bills = await _billService.GetAllAsync();
            return Ok(bills);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BillDto>> Get(int id)
        {
            var b = await _billService.GetByIdAsync(id);
            if (b == null) return NotFound();
            return Ok(b);
        }

        [HttpPost]
        public async Task<ActionResult<BillDto>> Create(CreateBillDto dto)
        {
            var b = await _billService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = b.Id }, b);
        }
    }
}
