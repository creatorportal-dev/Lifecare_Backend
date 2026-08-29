using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpdWardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IpdWardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/IpdWards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IpdWard>>> GetIpdWards()
        {
            return await _context.IpdWards.ToListAsync();
        }

        // GET: api/IpdWards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IpdWard>> GetIpdWard(int id)
        {
            var ipdWard = await _context.IpdWards.FindAsync(id);

            if (ipdWard == null)
            {
                return NotFound();
            }

            return ipdWard;
        }

        // PUT: api/IpdWards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIpdWard(int id, IpdWard ipdWard)
        {
            if (id != ipdWard.Id)
            {
                return BadRequest();
            }

            _context.Entry(ipdWard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IpdWardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/IpdWards
        [HttpPost]
        public async Task<ActionResult<IpdWard>> PostIpdWard(IpdWard ipdWard)
        {
            _context.IpdWards.Add(ipdWard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIpdWard), new { id = ipdWard.Id }, ipdWard);
        }

        // DELETE: api/IpdWards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIpdWard(int id)
        {
            var ipdWard = await _context.IpdWards.FindAsync(id);
            if (ipdWard == null)
            {
                return NotFound();
            }

            _context.IpdWards.Remove(ipdWard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IpdWardExists(int id)
        {
            return _context.IpdWards.Any(e => e.Id == id);
        }
    }
}
