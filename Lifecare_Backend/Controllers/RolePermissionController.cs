using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lifecare_Backend.Data;
using Lifecare_Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RolePermissionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolePermission>>> GetRolePermissions()
        {
            return await _context.RolePermissions.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<RolePermission>> SaveRolePermission(RolePermission payload)
        {
            var existing = await _context.RolePermissions.FirstOrDefaultAsync(p =>
                p.RoleName == payload.RoleName &&
                p.ModuleName == payload.ModuleName &&
                p.ActionName == payload.ActionName);

            if (existing != null)
            {
                existing.IsAllowed = payload.IsAllowed;
                existing.IsLocked = payload.IsLocked;
            }
            else
            {
                _context.RolePermissions.Add(payload);
            }

            await _context.SaveChangesAsync();
            return Ok(payload);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolePermission(int id)
        {
            var rolePermission = await _context.RolePermissions.FindAsync(id);
            if (rolePermission == null)
            {
                return NotFound();
            }

            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("lock/{roleName}")]
        public async Task<IActionResult> ToggleRoleLock(string roleName, [FromBody] bool isLocked)
        {
            var permissions = await _context.RolePermissions.Where(p => p.RoleName == roleName).ToListAsync();
            foreach (var permission in permissions)
            {
                permission.IsLocked = isLocked;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("rename")]
        public async Task<IActionResult> RenameRole([FromBody] RenameRoleRequest request)
        {
            var permissions = await _context.RolePermissions.Where(p => p.RoleName == request.OldName).ToListAsync();
            foreach (var permission in permissions)
            {
                permission.RoleName = request.NewName;
            }
            
            // Also rename in Employee designation/role if necessary
            var employees = await _context.Employees.Where(e => e.Role == request.OldName).ToListAsync();
            foreach (var emp in employees)
            {
                emp.Role = request.NewName;
            }
            
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class RenameRoleRequest
    {
        public string OldName { get; set; }
        public string NewName { get; set; }
    }
}
