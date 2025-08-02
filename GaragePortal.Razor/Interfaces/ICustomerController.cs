using Microsoft.AspNetCore.Mvc;

namespace GaragePortal.Razor.Interfaces
{
    public interface ICustomerController
    {
        Task<IActionResult> Index();
    }
} 