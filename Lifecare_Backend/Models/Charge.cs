using System.ComponentModel.DataAnnotations;

namespace Lifecare_Backend.Models
{
    public class Charge
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }
    }

    public class CreateChargeDto
    {
        [Required]
        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Amount { get; set; }
    }

    public class UpdateChargeDto
    {
        [StringLength(250)]
        public string? Name { get; set; }
        
        public decimal? Amount { get; set; }
    }

    public class ChargeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
