using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaragePortal.Domain.Entities;
using GaragePortal.Domain.Data;
using GaragePortal.Razor.Models;
using GaragePortal.Razor.Interfaces;

namespace GaragePortal.Razor.Controllers
{
    public class MechanicController : BaseEntity, IMechanicController
    {
        private readonly MyDbContext context;

        public MechanicController(MyDbContext context)
        {
            this.context = context;
        }



       

        [HttpPost]
        public async Task<IActionResult> CompleteService(int id, string repairDescription, decimal hours)
        {
            if (!IsMechanic())
                return RedirectToLogin();

            var service = context.Services
                .Include(s => s.Vehicle)
                .FirstOrDefault(s => s.RepairId == id);

            if (service == null)
            {
                return NotFound();
            }

            service.RepairDescription = repairDescription;
            service.Hours = hours;
            service.RepairStatus = "Completed";
            service.FinishDate = DateTime.UtcNow;
            service.RepairCost = hours * 75;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error: {ex.InnerException?.Message}");
                throw;
            }

            return RedirectToAction("Index");
        }
         public async Task<IActionResult> Index()
        {
            if (!IsMechanic())
                return RedirectToLogin();

            int mechanicId = GetCurrentUserId();
            if (mechanicId == 0)
                return RedirectToLogin();

            var services = await context.Services
                .Include(s => s.Vehicle)
                .Include(s => s.Technician)
                .Where(s => s.TechnicianId == mechanicId)
                .Select(s => new ServiceDTO
                {
                    RepairId = s.RepairId,
                    RepairDate = s.RepairDate,
                    TechnicianName = s.Technician != null ? s.Technician.TechnicianName : string.Empty,
                    EstimatedWorkDescription = s.EstimatedWorkDescription,
                    RepairDescription = s.RepairDescription,
                    Hours = s.Hours,
                    VehicleId = s.VehicleId,
                    TechnicianId = s.TechnicianId,
                    RepairCost = s.RepairCost,
                    LicensePlate = s.Vehicle != null ? s.Vehicle.LicensePlate : string.Empty,
                    RepairStatus = s.RepairStatus ?? "Pending",
                    FinishDate = s.FinishDate
                })
                .ToListAsync();

            return View(services);
        }
    }
}