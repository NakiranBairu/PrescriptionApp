using System.ComponentModel.DataAnnotations;

namespace PrescriptionApp.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        
        [Required(ErrorMessage = "Medication Name is required")]
        [StringLength(100, ErrorMessage = "Medication Name cannot exceed 100 characters")]
        public string MedicationName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Fill Status is required")]
        public string FillStatus { get; set; } = "New";
        
        [Required(ErrorMessage = "Cost is required")]
        [Range(0.01, 10000, ErrorMessage = "Cost must be between 0.01 and 10,000")]
        public decimal Cost { get; set; }
        
        public DateTime RequestTime { get; set; } = DateTime.Now;
    }
}