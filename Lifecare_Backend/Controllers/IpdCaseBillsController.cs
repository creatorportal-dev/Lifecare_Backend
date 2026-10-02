using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpdCaseBillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IpdCaseBillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var bills = await _context.IpdCaseBills
                .Include(b => b.Items)
                .Where(b => b.PatientId == patientId)
                .OrderByDescending(b => b.BillDate)
                .ToListAsync();
            return Ok(bills);
        }

        [HttpPost]
        public async Task<IActionResult> Create(IpdCaseBill bill)
        {
            _context.IpdCaseBills.Add(bill);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByPatient), new { patientId = bill.PatientId }, bill);
        }
    }
}
