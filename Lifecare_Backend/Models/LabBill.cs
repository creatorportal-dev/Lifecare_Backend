using System;
using System.Collections.Generic;

namespace Lifecare_Backend.Models
{
    public class LabBill
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string ReceiptNo { get; set; } = string.Empty;
        public DateTime BillDate { get; set; } = DateTime.UtcNow;
        public string Ward { get; set; } = string.Empty;
        public string BedCategory { get; set; } = string.Empty;
        
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }

        public List<LabBillItem> Items { get; set; } = new();
    }

    public class LabBillItem
    {
        public int Id { get; set; }
        public int LabBillId { get; set; }
        public string Description { get; set; } = string.Empty; 
        public int DaysQty { get; set; }
        public decimal Charges { get; set; }
        public decimal TotalCharges { get; set; }
    }
}
