using Microsoft.AspNetCore.Mvc;
using GaragePortal.Domain.Entities;

namespace GaragePortal.Razor.Interfaces
{
    public interface IAdminController
    {
        Task<IActionResult> Index();
        Task<IActionResult> AddCustomer(Customer customer);
        Task<IActionResult> DeleteCustomer(int id);
        Task<IActionResult> CustomerDetails(int id);
    }
} 