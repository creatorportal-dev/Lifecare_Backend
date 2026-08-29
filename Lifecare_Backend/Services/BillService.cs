using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lifecare_Backend.Services
{
    public class BillService : IBillService
    {
        private readonly ApplicationDbContext _context;

        public BillService(ApplicationDbContext context)
        {
            _context = context;
        }

        private BillDto MapToDto(Bill b)
        {
            return new BillDto
            {
                Id = b.Id,
                PatientId = b.Patient.Id.ToString(), // Map to string for frontend
                PatientCode = b.PatientCode,
                Subtotal = b.Subtotal,
                DiscountType = b.DiscountType,
                DiscountValue = b.DiscountValue,
                Total = b.Total,
                CreatedAt = b.CreatedAt.ToString("o"),
                Items = b.Items.Select(i => new BillItemDto
                {
                    MedicineId = i.MedicineId.ToString(),
                    Name = i.Name,
                    Units = i.Units,
                    Pieces = i.Pieces,
                    Mrp = i.Mrp,
                    Total = i.Total
                }).ToList()
            };
        }

        public async Task<IEnumerable<BillDto>> GetAllAsync()
        {
            var bills = await _context.Bills
                .Include(b => b.Patient)
                .Include(b => b.Items)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return bills.Select(MapToDto);
        }

        public async Task<BillDto?> GetByIdAsync(int id)
        {
            var b = await _context.Bills
                .Include(x => x.Patient)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (b == null) return null;
            return MapToDto(b);
        }

        public async Task<BillDto> CreateAsync(CreateBillDto dto)
        {
            var patientId = int.Parse(dto.PatientId); // Assuming frontend passes the numeric ID as string

            var bill = new Bill
            {
                PatientId = patientId,
                PatientCode = dto.PatientCode,
                Subtotal = dto.Subtotal,
                DiscountType = dto.DiscountType,
                DiscountValue = dto.DiscountValue,
                Total = dto.Total,
                CreatedAt = DateTime.UtcNow,
                Items = dto.Items.Select(i => new BillItem
                {
                    MedicineId = int.Parse(i.MedicineId),
                    Name = i.Name,
                    Units = i.Units,
                    Pieces = i.Pieces,
                    Mrp = i.Mrp,
                    Total = i.Total
                }).ToList()
            };

            _context.Bills.Add(bill);

            // Deduct stock for each medicine billed
            foreach (var item in dto.Items)
            {
                var medicineId = int.Parse(item.MedicineId);
                var medicine = await _context.Medicines.FindAsync(medicineId);
                if (medicine != null)
                {
                    // Adjust stock based on pieces
                    medicine.Quantity = (int)Math.Max(0, medicine.Quantity - item.Pieces);
                }
            }

            await _context.SaveChangesAsync();

            // Load related patient for mapping
            await _context.Entry(bill).Reference(b => b.Patient).LoadAsync();

            return MapToDto(bill);
        }
    }
}
