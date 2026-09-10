using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Lifecare_Backend.Services
{
    public class EmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<EmployeeDto>> GetAllAsync(int page = 1, int pageSize = 50)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 1000);

            var query = _context.Employees.AsNoTracking().OrderBy(e => e.Id);
            var total = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Code = e.Code,
                    Name = e.Name,
                    Email = e.Email,
                    Password = e.Password,
                    Phone = e.Phone,
                    Department = e.Department,
                    Role = e.Role,
                    JoiningDate = e.JoiningDate,
                    Address = e.Address,
                    Photo = e.Photo,
                    Active = e.Active
                })
                .ToListAsync();

            return new PagedResult<EmployeeDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var e = await _context.Employees
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Email = x.Email,
                    Password = x.Password,
                    Phone = x.Phone,
                    Department = x.Department,
                    Role = x.Role,
                    JoiningDate = x.JoiningDate,
                    Address = x.Address,
                    Photo = x.Photo,
                    Active = x.Active
                })
                .FirstOrDefaultAsync();

            return e;
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            int nextSeq = 1;
            var lastEmployee = await _context.Employees
                .OrderByDescending(e => e.Id)
                .FirstOrDefaultAsync();
            if (lastEmployee != null)
            {
                nextSeq = lastEmployee.Id + 1;
            }
            
            string code = $"EMP-{nextSeq.ToString().PadLeft(4, '0')}";

            var employee = new Employee
            {
                Code = code,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Phone = dto.Phone ?? string.Empty,
                Department = dto.Department ?? string.Empty,
                Role = dto.Role,
                JoiningDate = dto.JoiningDate,
                Address = dto.Address ?? string.Empty,
                Photo = dto.Photo,
                Active = dto.Active
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return new EmployeeDto
            {
                Id = employee.Id,
                Code = employee.Code,
                Name = employee.Name,
                Email = employee.Email,
                Password = employee.Password,
                Phone = employee.Phone,
                Department = employee.Department,
                Role = employee.Role,
                JoiningDate = employee.JoiningDate,
                Address = employee.Address,
                Photo = employee.Photo,
                Active = employee.Active
            };
        }

        public async Task<EmployeeDto?> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var e = await _context.Employees.FindAsync(id);
            if (e == null) return null;

            e.Name = dto.Name;
            e.Email = dto.Email;
            e.Phone = dto.Phone ?? string.Empty;
            e.Department = dto.Department ?? string.Empty;
            e.Role = dto.Role;
            e.JoiningDate = dto.JoiningDate;
            e.Address = dto.Address ?? string.Empty;
            e.Photo = dto.Photo;
            e.Active = dto.Active;

            await _context.SaveChangesAsync();

            return new EmployeeDto
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Email = e.Email,
                Password = e.Password,
                Phone = e.Phone,
                Department = e.Department,
                Role = e.Role,
                JoiningDate = e.JoiningDate,
                Address = e.Address,
                Photo = e.Photo,
                Active = e.Active
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await _context.Employees.FindAsync(id);
            if (e == null) return false;

            _context.Employees.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<EmployeeDto?> ToggleActiveAsync(int id)
        {
            var e = await _context.Employees.FindAsync(id);
            if (e == null) return null;

            e.Active = !e.Active;
            await _context.SaveChangesAsync();

            return new EmployeeDto
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Email = e.Email,
                Password = e.Password,
                Phone = e.Phone,
                Department = e.Department,
                Role = e.Role,
                JoiningDate = e.JoiningDate,
                Address = e.Address,
                Photo = e.Photo,
                Active = e.Active
            };
        }

        public async Task<EmployeeDto?> LoginAsync(LoginDto dto)
        {
            var lowerEmail = dto.Email?.ToLower() ?? string.Empty;
            var e = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.ToLower() == lowerEmail && x.Password == dto.Password && x.Active);
            if (e == null) return null;

            return new EmployeeDto
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Email = e.Email,
                Password = e.Password,
                Phone = e.Phone,
                Department = e.Department,
                Role = e.Role,
                JoiningDate = e.JoiningDate,
                Address = e.Address,
                Photo = e.Photo,
                Active = e.Active
            };
        }
    }
}
