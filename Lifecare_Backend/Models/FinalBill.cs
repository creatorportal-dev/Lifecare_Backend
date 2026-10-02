using System;
using System.Collections.Generic;

namespace Lifecare_Backend.Models
{
    public class FinalBill
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string ReceiptNo { get; set; } = string.Empty;
        public DateTime BillDate { get; set; } = DateTime.UtcNow;
        public DateTime? DischargeDate { get; set; }
        public string Ward { get; set; } = string.Empty;
        public string BedCategory { get; set; } = string.Empty;
        
        public decimal TotalAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal NetAmount { get; set; }
        
        public string DepositDateRange { get; set; } = string.Empty;

        public List<FinalBillItem> Items { get; set; } = new();
    }

    public class FinalBillItem
    {
        public int Id { get; set; }
        public int FinalBillId { get; set; }
        public string Description { get; set; } = string.Empty; 
        public int DaysQty { get; set; }
        public decimal Charges { get; set; }
        public decimal TotalCharges { get; set; }
    }
}
