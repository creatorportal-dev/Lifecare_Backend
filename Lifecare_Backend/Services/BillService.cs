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

        public async Task<PagedResult<BillDto>> GetAllAsync(int page = 1, int pageSize = 50)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 1000);

            var query = _context.Bills.AsNoTracking().OrderByDescending(b => b.CreatedAt);
            var total = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(b => new BillDto
                {
                    Id = b.Id,
                    PatientId = b.PatientId.ToString(),
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
                })
                .ToListAsync();

            return new PagedResult<BillDto> { Items = items, Page = page, PageSize = pageSize, TotalCount = total };
        }

        public async Task<BillDto?> GetByIdAsync(int id)
        {
            var b = await _context.Bills
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new BillDto
                {
                    Id = x.Id,
                    PatientId = x.PatientId.ToString(),
                    PatientCode = x.PatientCode,
                    Subtotal = x.Subtotal,
                    DiscountType = x.DiscountType,
                    DiscountValue = x.DiscountValue,
                    Total = x.Total,
                    CreatedAt = x.CreatedAt.ToString("o"),
                    Items = x.Items.Select(i => new BillItemDto
                    {
                        MedicineId = i.MedicineId.ToString(),
                        Name = i.Name,
                        Units = i.Units,
                        Pieces = i.Pieces,
                        Mrp = i.Mrp,
                        Total = i.Total
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return b;
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

            // Project created bill to DTO (avoid tracking full entity)
            var result = await _context.Bills
                .AsNoTracking()
                .Where(b => b.Id == bill.Id)
                .Select(b => new BillDto
                {
                    Id = b.Id,
                    PatientId = b.PatientId.ToString(),
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
                })
                .FirstOrDefaultAsync();

            return result!;
        }
    }
}
