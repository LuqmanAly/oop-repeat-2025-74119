using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaragePortal.Domain.Entities;
using GaragePortal.Domain.Data;
using GaragePortal.Razor.Interfaces;

namespace GaragePortal.Razor.Controllers
{
    public class CarController : BaseEntity, ICarController
    {
        private readonly MyDbContext context;

        public CarController(MyDbContext context)
        {
            this.context = context;
        }



       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCar(int id)
        {
            if (!IsAdmin())
                return RedirectToLogin();

            var car = context.Cars.Include(c => c.Repairs)
                                   .FirstOrDefault(c => c.VehicleId == id);

            if (car == null)
            {
                return NotFound();
            }

            if (car.Repairs.Any())
            {
                context.Services.RemoveRange(car.Repairs);
            }

            context.Cars.Remove(car);
            context.SaveChanges();

            return Ok();
        }
         [HttpPost]
        public IActionResult AddNewCar(string LicensePlate, int ClientId)
        {
            if (!IsAdmin())
                return RedirectToLogin();

            var exists = context.Cars.Any(c => c.LicensePlate == LicensePlate && c.ClientId == ClientId);

            if (exists)
            {
                TempData["CarExists"] = "Car with this license plate already exists.";
                return RedirectToAction("CustomerDetails", "Admin", new { id = ClientId });
            }

            var newCar = new Car
            {
                LicensePlate = LicensePlate,
                ClientId = ClientId
            };

            context.Cars.Add(newCar);
            context.SaveChanges();

            return RedirectToAction("CustomerDetails", "Admin", new { id = ClientId });
        }

    }
}