using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaragePortal.Domain.Entities;
using GaragePortal.Domain.Data;
using GaragePortal.Razor.Models;
using GaragePortal.Razor.Interfaces;

namespace GaragePortal.Razor.Controllers
{
    public class CustomerController : BaseEntity, ICustomerController
    {
        private readonly MyDbContext context;

        public CustomerController(MyDbContext context)
        {
            this.context = context;
        }



        public async Task<IActionResult> Index()
        {
            if (!IsCustomer())
                return RedirectToLogin();

            int customerId = GetCurrentUserId();
            if (customerId == 0)
                return RedirectToLogin();

            var customerCars = await context.Cars
                .Where(car => car.ClientId == customerId)
                .Include(car => car.Repairs)
                    .ThenInclude(service => service.Technician)
                .ToListAsync();

            var carDTOs = customerCars.Select(car => new CarDTO
            {
                VehicleId = car.VehicleId,
                LicensePlate = car.LicensePlate,
                Repairs = car.Repairs.Select(s => new ServiceDTO
                {
                    RepairId = s.RepairId,
                    RepairDate = s.RepairDate,
                    TechnicianName = s.Technician != null ? s.Technician.TechnicianName : "N/A",
                    EstimatedWorkDescription = s.EstimatedWorkDescription,
                    RepairDescription = s.RepairDescription,
                    RepairStatus = s.RepairStatus,
                    Hours = s.Hours,
                    FinishDate = s.FinishDate,
                    RepairCost = s.RepairCost
                }).ToList()
            }).ToList();

            return View(carDTOs);
        }
    }
}