using Microsoft.AspNetCore.Mvc;
using GaragePortal.Domain.Entities;

namespace GaragePortal.Razor.Interfaces
{
    public interface IHomeController
    {
        IActionResult Index();
        IActionResult Login();
        IActionResult Signup();
        IActionResult Privacy();
        IActionResult Dashboard();
        Task<IActionResult> Login(Customer user);
        IActionResult UserSession();
        IActionResult Logout();
        IActionResult Error();
    }
} 