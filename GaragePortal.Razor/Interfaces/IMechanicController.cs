using Microsoft.AspNetCore.Mvc;

namespace GaragePortal.Razor.Interfaces
{
    public interface IMechanicController
    {
        Task<IActionResult> CompleteService(int id, string repairDescription, decimal hours);
        Task<IActionResult> Index();
    }
} 