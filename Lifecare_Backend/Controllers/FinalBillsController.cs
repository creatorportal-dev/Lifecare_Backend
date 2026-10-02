using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Lifecare_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinalBillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FinalBillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var bills = await _context.FinalBills
                .Include(b => b.Items)
                .Where(b => b.PatientId == patientId)
                .OrderByDescending(b => b.BillDate)
                .ToListAsync();
            return Ok(bills);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FinalBill bill)
        {
            if (bill.Items == null || !bill.Items.Any())
            {
                return BadRequest("Bill must contain at least one item.");
            }

            _context.FinalBills.Add(bill);
            await _context.SaveChangesAsync();
            return Ok(bill);
        }
    }
}
