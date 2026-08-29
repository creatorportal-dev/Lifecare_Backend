using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lifecare_Backend.Data;
using Lifecare_Backend.Models;

namespace Lifecare_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public HospitalSettingsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/HospitalSettings
        [HttpGet]
        public async Task<ActionResult<HospitalSettings>> GetSettings()
        {
            var settings = await _context.HospitalSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new HospitalSettings 
                { 
                    Helpline = "93 74 108 108 / 8000 8111", 
                    Address = "Vijardiya", 
                    LogoUrl = "/logo.png" 
                };
                _context.HospitalSettings.Add(settings);
                await _context.SaveChangesAsync();
            }
            return settings;
        }

        // PUT: api/HospitalSettings
        [HttpPut]
        public async Task<IActionResult> UpdateSettings(HospitalSettings updatedSettings)
        {
            var settings = await _context.HospitalSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                _context.HospitalSettings.Add(updatedSettings);
            }
            else
            {
                settings.Helpline = updatedSettings.Helpline;
                settings.Address = updatedSettings.Address;
                settings.LogoUrl = updatedSettings.LogoUrl;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/HospitalSettings/upload-logo
        [HttpPost("upload-logo")]
        public async Task<IActionResult> UploadLogo(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var logoPath = _configuration["LogoUploadPath"] ?? "c:/logo";
            if (!Directory.Exists(logoPath))
            {
                Directory.CreateDirectory(logoPath);
            }

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"logo_{DateTime.Now.Ticks}{extension}";
            var filePath = Path.Combine(logoPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var logoUrl = $"/logo/{fileName}";

            var settings = await _context.HospitalSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new HospitalSettings 
                { 
                    Helpline = "", 
                    Address = "", 
                    LogoUrl = logoUrl 
                };
                _context.HospitalSettings.Add(settings);
            }
            else
            {
                settings.LogoUrl = logoUrl;
            }

            await _context.SaveChangesAsync();

            return Ok(new { LogoUrl = logoUrl });
        }
    }
}
