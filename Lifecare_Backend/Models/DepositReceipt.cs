using System;
using System.Collections.Generic;

namespace Lifecare_Backend.Models
{
    public class DepositReceipt
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string ReceiptNo { get; set; } = string.Empty;
        public DateTime BillDate { get; set; } = DateTime.UtcNow;
        public DateTime? DischargeDate { get; set; }
        public string Ward { get; set; } = string.Empty;
        public string BedCategory { get; set; } = string.Empty;
        
        public decimal FinalDepositAmount { get; set; }
        public decimal AdditionalAmount { get; set; }
        public decimal NetAmount { get; set; }

        public List<DepositItem> Items { get; set; } = new();
    }

    public class DepositItem
    {
        public int Id { get; set; }
        public int DepositReceiptId { get; set; }
        public string Description { get; set; } = string.Empty; // e.g. "Hospital 1st Deposit (01/02/2022)"
        public string ItemReceiptNo { get; set; } = string.Empty; // e.g. "1", "0"
        public decimal Charges { get; set; }
        public decimal TotalCharges { get; set; }
    }
}
