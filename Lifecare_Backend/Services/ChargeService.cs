using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Lifecare_Backend.Services
{
    public class ChargeService : IChargeService
    {
        private readonly ApplicationDbContext _context;

        public ChargeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChargeDto>> GetAllAsync()
        {
            return await _context.Charges
                .Select(c => new ChargeDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Amount = c.Amount
                })
                .ToListAsync();
        }

        public async Task<ChargeDto?> GetByIdAsync(int id)
        {
            var charge = await _context.Charges.FindAsync(id);
            if (charge == null) return null;

            return new ChargeDto
            {
                Id = charge.Id,
                Name = charge.Name,
                Amount = charge.Amount
            };
        }

        public async Task<ChargeDto> CreateAsync(CreateChargeDto dto)
        {
            var charge = new Charge
            {
                Name = dto.Name,
                Amount = dto.Amount
            };

            _context.Charges.Add(charge);
            await _context.SaveChangesAsync();

            return new ChargeDto
            {
                Id = charge.Id,
                Name = charge.Name,
                Amount = charge.Amount
            };
        }

        public async Task<ChargeDto?> UpdateAsync(int id, UpdateChargeDto dto)
        {
            var charge = await _context.Charges.FindAsync(id);
            if (charge == null) return null;

            if (dto.Name != null) charge.Name = dto.Name;
            if (dto.Amount != null) charge.Amount = dto.Amount.Value;

            await _context.SaveChangesAsync();

            return new ChargeDto
            {
                Id = charge.Id,
                Name = charge.Name,
                Amount = charge.Amount
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var charge = await _context.Charges.FindAsync(id);
            if (charge == null) return false;

            _context.Charges.Remove(charge);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
