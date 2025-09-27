using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrescriptionApp.Models;

namespace PrescriptionApp.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddEdit(int? id)
        {
            ViewBag.FillStatuses = GetFillStatuses();
            
            if (id.HasValue)
            {
                var prescription = _context.Prescriptions.Find(id.Value);
                if (prescription == null) return RedirectToAction("Index", "Home");
                return View(prescription);
            }
            
            return View(new Prescription());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEdit(int id, Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    prescription.RequestTime = DateTime.Now;
                    _context.Prescriptions.Add(prescription);
                }
                else
                {
                    var existing = _context.Prescriptions.Find(id);
                    if (existing == null) return RedirectToAction("Index", "Home");
                    
                    existing.MedicationName = prescription.MedicationName;
                    existing.FillStatus = prescription.FillStatus;
                    existing.Cost = prescription.Cost;
                }
                
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.FillStatuses = GetFillStatuses();
            return View(prescription);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var prescription = _context.Prescriptions.Find(id);
            if (prescription == null) return RedirectToAction("Index", "Home");
            return View(prescription);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var prescription = _context.Prescriptions.Find(id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        private SelectList GetFillStatuses()
        {
            return new SelectList(new[] { "New", "Filled", "Pending" });
        }
    }
}