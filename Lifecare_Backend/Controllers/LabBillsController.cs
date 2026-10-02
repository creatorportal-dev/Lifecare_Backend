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
    public class LabBillsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LabBillsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var bills = await _context.LabBills
                .Include(b => b.Items)
                .Where(b => b.PatientId == patientId)
                .OrderByDescending(b => b.BillDate)
                .ToListAsync();
            return Ok(bills);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LabBill bill)
        {
            if (bill.Items == null || !bill.Items.Any())
            {
                return BadRequest("Bill must contain at least one item.");
            }

            _context.LabBills.Add(bill);
            await _context.SaveChangesAsync();
            return Ok(bill);
        }
    }
}
