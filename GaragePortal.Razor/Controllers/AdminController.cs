
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaragePortal.Domain.Entities;
using GaragePortal.Domain.Data;
using GaragePortal.Razor.Models;
using GaragePortal.Razor.Interfaces;

namespace GaragePortal.Razor.Controllers
{
    public class AdminController : BaseEntity, IAdminController
    {
        private readonly MyDbContext context;

        public AdminController(MyDbContext context)
        {
            this.context = context;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!IsAdmin())
                return RedirectToLogin();

            var customers = await context.Customers
                .Select(c => new CustomerDTO
                {
                    ClientId = c.ClientId,
                    ClientName = c.ClientName,
                    ClientEmail = c.ClientEmail,
                    ClientPassword = c.ClientPassword,
                }).ToListAsync();

            return View(customers);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            if (!IsAdmin())
                return RedirectToLogin();

            bool emailExists = await context.Customers
                .AnyAsync(c => c.ClientEmail == customer.ClientEmail);

            if (emailExists)
            {
                ModelState.AddModelError("ClientEmail", "This email is already registered");
            }

            if (!ModelState.IsValid)
            {
                var allCustomers = await context.Customers
                    .Select(c => new CustomerDTO
                    {
                        ClientId = c.ClientId,
                        ClientName = c.ClientName,
                        ClientEmail = c.ClientEmail,
                        ClientPassword = c.ClientPassword
                    })
                    .ToListAsync();

                ViewBag.NewCustomer = customer;
                return View("Index", allCustomers);
            }

            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            TempData["Success"] = "Customer added successfully!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!IsAdmin())
                return RedirectToLogin();

            var customer = context.Customers.Find(id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
                TempData["Success"] = "Customer deleted successfully.";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CustomerDetails(int id)
        {
            if (!IsAdmin())
                return RedirectToLogin();

            var cars = await context.Cars
                .Include(c => c.Client)
                .Include(c => c.Repairs)
                .Where(c => c.ClientId == id)
                .Select(c => new CarDTO
                {
                    VehicleId = c.VehicleId,
                    LicensePlate = c.LicensePlate,
                    ClientId = c.ClientId,
                    Repairs = c.Repairs.Select(s => new ServiceDTO
                    {
                        RepairId = s.RepairId
                    }).ToList()
                })
                .ToListAsync();

            ViewBag.CustomerId = id;

            var customerName = await context.Customers
                .Where(c => c.ClientId == id)
                .Select(e => e.ClientName)
                .FirstOrDefaultAsync();

            ViewBag.Name = customerName;

            ViewBag.Mechanics = await context.Mechanics
                .Select(m => new MechanicDTO
                {
                    TechnicianId = m.TechnicianId,
                    TechnicianName = m.TechnicianName
                })
                .ToListAsync();

            return View(cars);
        }
    }
}

