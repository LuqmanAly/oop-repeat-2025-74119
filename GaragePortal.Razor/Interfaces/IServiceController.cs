using Microsoft.AspNetCore.Mvc;
using GaragePortal.Domain.Entities;

namespace GaragePortal.Razor.Interfaces
{
    public interface IServiceController
    {
        IActionResult GetServicesForCar(int carId);
        IActionResult AddServiceToCar(Service service);
        IActionResult Delete(int id);
    }
} 