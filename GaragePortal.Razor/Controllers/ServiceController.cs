using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaragePortal.Domain.Entities;
using GaragePortal.Domain.Data;
using GaragePortal.Razor.Models;
using GaragePortal.Razor.Interfaces;

namespace GaragePortal.Razor.Controllers
{
    public class ServiceController : BaseEntity, IServiceController
    {
        private readonly MyDbContext context;

        public ServiceController(MyDbContext context)
        {
            this.context = context;
        }



       

        [HttpGet]
        public IActionResult GetServicesForCar(int carId)
        {
            try
            {
                var services = context.Services
                    .Where(s => s.VehicleId == carId)
                    .Include(s => s.Technician)
                    .Select(s => new
                    {
                        RepairId = s.RepairId,
                        RepairDate = s.RepairDate,
                        TechnicianName = s.Technician != null ? s.Technician.TechnicianName : null,
                        EstimatedWorkDescription = s.EstimatedWorkDescription,
                        RepairDescription = s.RepairDescription,
                        Hours = s.Hours
                    })
                    .ToList();

                return Json(services);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetServicesForCar: {ex.Message}");
                return StatusCode(500, "Error fetching services");
            }
        }
         [HttpPost]
        public IActionResult AddServiceToCar(Service service)
        {
            if (!IsAdmin() && !IsMechanic())
                return RedirectToLogin();

            service.Hours = 0; // Admin doesn't set hours, mechanics will add actual hours
            service.CalculateCost();
            service.RepairDate = DateTime.SpecifyKind(service.RepairDate, DateTimeKind.Utc);
            service.RepairStatus = "Pending";

            context.Services.Add(service);
            context.SaveChanges();

            var customerId = context.Cars
                .Where(c => c.VehicleId == service.VehicleId)
                .Select(c => c.ClientId)
                .FirstOrDefault();

            return RedirectToAction("CustomerDetails", "Admin", new { id = customerId });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin() && !IsMechanic())
                return RedirectToLogin();

            var service = context.Services.FirstOrDefault(s => s.RepairId == id);
            if (service != null)
            {
                context.Services.Remove(service);
                context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
