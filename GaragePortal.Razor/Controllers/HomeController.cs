using GaragePortal.Domain.Entities;
using GaragePortal.Domain.Data;
using GaragePortal.Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using GaragePortal.Razor.Interfaces;

namespace GaragePortal.Razor.Controllers
    {
        public class HomeController : BaseEntity, IHomeController
        {
            private readonly MyDbContext context;

            public HomeController(MyDbContext context)
            {
                this.context = context;
            }

            public IActionResult Index()
            {
                return View();
            }

            public IActionResult Login()
            {
                if (IsAuthenticated())
                {
                    return RedirectToAction("Dashboard");
                }
                return View();
            }



            public IActionResult Signup()
            {
                List<SelectListItem> Gender = new()
            {
                new SelectListItem {Value="Male",Text="Male"},
                new SelectListItem {Value="Female",Text="Female"}
            };
                ViewBag.Gender = Gender;
                return View();
            }
            public IActionResult Privacy()
            {
                return View();
            }

            private async Task AddDefaultMechanics()
            {
                if (!context.Mechanics.Any(u => u.TechnicianEmail == "mechanic1@carservice.com"))
                {
                    context.Mechanics.Add(new Mechanic
                    {
                        TechnicianName = "Mechanic 1",
                        TechnicianEmail = "mechanic1@carservice.com",
                        TechnicianPassword = "Dorset001^",
                    });
                }

                if (!context.Mechanics.Any(u => u.TechnicianEmail == "mechanic2@carservice.com"))
                {
                    context.Mechanics.Add(new Mechanic
                    {
                        TechnicianName = "Mechanic 2",
                        TechnicianEmail = "mechanic2@carservice.com",
                        TechnicianPassword = "Dorset001^",
                    });
                }

                if (!context.Admins.Any(u => u.Email == "admin@carservice.com"))
                {
                    context.Admins.Add(new Admin
                    {
                        AdminName = "Admin",
                        Email = "admin@carservice.com",
                        Password = "Dorset001^",
                    });
                }

                if (!context.Customers.Any(u => u.ClientEmail == "customer1@carservice.com"))
                {
                    context.Customers.Add(new Customer
                    {
                        ClientName = "Customer 1",
                        ClientEmail = "customer1@carservice.com",
                        ClientPassword = "Dorset001^",
                    });
                }

                if (!context.Customers.Any(u => u.ClientEmail == "customer2@carservice.com"))
                {
                    context.Customers.Add(new Customer
                    {
                        ClientName = "Customer 2",
                        ClientEmail = "customer2@carservice.com",
                        ClientPassword = "Dorset001^",
                    });
                }

                await context.SaveChangesAsync();
            }

            public IActionResult Dashboard()
            {
                if (IsAuthenticated())
                {
                    ViewData["MySession"] = GetSessionData("UserSession");
                    var userRole = GetCurrentUserRole();

                    // Redirect based on role
                    return userRole switch
                    {
                        "Admin" => RedirectToAction("Index", "Admin"),
                        "Mechanic" => RedirectToAction("Index", "Mechanic"),
                        "Customer" => RedirectToAction("Index", "Customer"),
                        _ => View()
                    };
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            [HttpPost]
            public async Task<IActionResult> Login(Customer user)
            {
                string AdminEmail = "admin@carservice.com";
                string Password = "Dorset001^";
                string MechanicEmail1 = "mechanic1@carservice.com";
                string MechanicEmail2 = "mechanic2@carservice.com";

                await AddDefaultMechanics();

                var customer = context.Customers.Where(x => x.ClientEmail == user.ClientEmail && x.ClientPassword == user.ClientPassword).FirstOrDefault();

                if (((user.ClientEmail == MechanicEmail1) || (user.ClientEmail == MechanicEmail2)) && (user.ClientPassword == Password))
                {
                    var mechanic = context.Mechanics.Where(x => x.TechnicianEmail == user.ClientEmail && x.TechnicianPassword == user.ClientPassword).FirstOrDefault();
                    HttpContext.Session.SetString("UserSession", $"{mechanic.TechnicianId}");
                    HttpContext.Session.SetString("UserRole", "Mechanic");
                    return RedirectToAction("Index", "Mechanic");
                }

                if ((user.ClientEmail == AdminEmail) && (user.ClientPassword == Password))
                {
                    HttpContext.Session.SetString("UserSession", "admin");
                    HttpContext.Session.SetString("UserRole", "Admin");
                    return RedirectToAction("Index", "Admin");
                }

                if (customer != null)
                {
                    HttpContext.Session.SetString("UserSession", $"{customer.ClientId}");
                    HttpContext.Session.SetString("UserRole", "Customer");
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ViewBag.Message = "Bad Credential Email or Password.";
                }

                return View();
            }
            public IActionResult UserSession()
            {
                if (IsAuthenticated())
                {
                    ClearSession();
                    return RedirectToAction("Login");
                }
                return RedirectToAction("Login");
            }

            public IActionResult Logout()
            {
                if (IsAuthenticated())
                {
                    ClearSession();
                    return RedirectToAction("Login");
                }
                return View();
            }



            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new GaragePortal.Domain.Entities.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }

