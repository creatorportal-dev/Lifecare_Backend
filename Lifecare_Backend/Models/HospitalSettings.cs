namespace Lifecare_Backend.Models
{
    public class HospitalSettings
    {
        public int Id { get; set; }
        public string Helpline { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public int FollowUpValidityMonths { get; set; } = 3;
        
        public string BankAccountName { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string BankIfscCode { get; set; } = string.Empty;
    }
}
