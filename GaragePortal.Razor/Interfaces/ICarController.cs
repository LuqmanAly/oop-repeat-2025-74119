using Microsoft.AspNetCore.Mvc;

namespace GaragePortal.Razor.Interfaces
{
    public interface ICarController
    {
        IActionResult DeleteCar(int id);
        IActionResult AddNewCar(string LicensePlate, int ClientId);
    }
} 