using Microsoft.EntityFrameworkCore;
using Lifecare_Backend.Models;

namespace Lifecare_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<MedicineCategory> MedicineCategories { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PastOperation> PastOperations { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescribedMedicine> PrescribedMedicines { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<HospitalSettings> HospitalSettings { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<IpdWard> IpdWards { get; set; }
        public DbSet<NursingChart> NursingCharts { get; set; }
        public DbSet<NursingChartMedication> NursingChartMedications { get; set; }
        
        public DbSet<DepositReceipt> DepositReceipts { get; set; }
        public DbSet<DepositItem> DepositItems { get; set; }
        
        public DbSet<IpdCaseBill> IpdCaseBills { get; set; }
        public DbSet<IpdCaseBillItem> IpdCaseBillItems { get; set; }

        public DbSet<LabBill> LabBills { get; set; }
        public DbSet<LabBillItem> LabBillItems { get; set; }

        public DbSet<FinalBill> FinalBills { get; set; }
        public DbSet<FinalBillItem> FinalBillItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepositReceipt>()
                .HasMany(d => d.Items)
                .WithOne()
                .HasForeignKey(i => i.DepositReceiptId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<IpdCaseBill>()
                .HasMany(d => d.Items)
                .WithOne()
                .HasForeignKey(i => i.IpdCaseBillId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LabBill>()
                .HasMany(d => d.Items)
                .WithOne()
                .HasForeignKey(i => i.LabBillId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<FinalBill>()
                .HasMany(d => d.Items)
                .WithOne()
                .HasForeignKey(i => i.FinalBillId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
