using Microsoft.EntityFrameworkCore;

namespace PrescriptionApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Prescription> Prescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    PrescriptionId = 1,
                    MedicationName = "Atorvastatin",
                    FillStatus = "Filled",
                    Cost = 19.99m,
                    RequestTime = DateTime.Now.AddDays(-5)
                },
                new Prescription
                {
                    PrescriptionId = 2,
                    MedicationName = "Lisinopril",
                    FillStatus = "Pending",
                    Cost = 12.50m,
                    RequestTime = DateTime.Now.AddDays(-2)
                },
                new Prescription
                {
                    PrescriptionId = 3,
                    MedicationName = "Metformin",
                    FillStatus = "New",
                    Cost = 8.75m,
                    RequestTime = DateTime.Now.AddDays(-1)
                }
            );
        }
    }
}