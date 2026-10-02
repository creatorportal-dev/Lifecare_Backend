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
    public class DepositReceiptsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepositReceiptsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
        {
            var receipts = await _context.DepositReceipts
                .Include(d => d.Items)
                .Where(d => d.PatientId == patientId)
                .OrderByDescending(d => d.BillDate)
                .ToListAsync();
            return Ok(receipts);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepositReceipt receipt)
        {
            _context.DepositReceipts.Add(receipt);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByPatient), new { patientId = receipt.PatientId }, receipt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DepositReceipt updatedReceipt)
        {
            if (id != updatedReceipt.Id)
                return BadRequest();

            var existing = await _context.DepositReceipts
                .Include(d => d.Items)
                .FirstOrDefaultAsync(d => d.Id == id);
            
            if (existing == null) return NotFound();

            // Update scalar properties
            existing.ReceiptNo = updatedReceipt.ReceiptNo;
            existing.BillDate = updatedReceipt.BillDate;
            existing.DischargeDate = updatedReceipt.DischargeDate;
            existing.Ward = updatedReceipt.Ward;
            existing.BedCategory = updatedReceipt.BedCategory;
            existing.FinalDepositAmount = updatedReceipt.FinalDepositAmount;
            existing.AdditionalAmount = updatedReceipt.AdditionalAmount;
            existing.NetAmount = updatedReceipt.NetAmount;

            // Update items (naive replace)
            _context.DepositItems.RemoveRange(existing.Items);
            foreach (var item in updatedReceipt.Items)
            {
                item.Id = 0; // ensure it's treated as new
                item.DepositReceiptId = id;
                _context.DepositItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.DepositReceipts.FindAsync(id);
            if (existing == null) return NotFound();

            _context.DepositReceipts.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
