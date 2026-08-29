namespace Lifecare_Backend.Models
{
    public class HospitalSettings
    {
        public int Id { get; set; }
        public string Helpline { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
    }
}
