using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaragePortal.Domain.Data;
using GaragePortal.Domain.Entities;

namespace GaragePortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceHistoryController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ServiceHistoryController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetAll()
        {
            return await _context.Services
                .Include(s => s.Vehicle)
                .Include(s => s.Technician)
                .Select(s => new Service
                {
                    RepairId = s.RepairId,
                    VehicleId = s.VehicleId,
                    TechnicianId = s.TechnicianId,
                    RepairDate = s.RepairDate,
                    EstimatedWorkDescription = s.EstimatedWorkDescription,
                    RepairDescription = s.RepairDescription,
                    Hours = s.Hours,
                    FinishDate = s.FinishDate,
                    RepairCost = s.RepairCost,
                    RepairStatus = s.RepairStatus,
                    Vehicle = new Car
                    {
                        VehicleId = s.Vehicle.VehicleId,
                        LicensePlate = s.Vehicle.LicensePlate,
                        ClientId = s.Vehicle.ClientId
                    },
                    Technician = new Mechanic
                    {
                        TechnicianId = s.Technician.TechnicianId,
                        TechnicianName = s.Technician.TechnicianName
                    }
                })
                .ToListAsync();
        }

        [HttpGet("car/{carId}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetByCarId(int carId)
        {
            var services = await _context.Services
                .Include(s => s.Vehicle)
                .Include(s => s.Technician)
                .Where(s => s.VehicleId == carId)
                .Select(s => new Service
                {
                    RepairId = s.RepairId,
                    VehicleId = s.VehicleId,
                    TechnicianId = s.TechnicianId,
                    RepairDate = s.RepairDate,
                    EstimatedWorkDescription = s.EstimatedWorkDescription,
                    RepairDescription = s.RepairDescription,
                    Hours = s.Hours,
                    FinishDate = s.FinishDate,
                    RepairCost = s.RepairCost,
                    RepairStatus = s.RepairStatus,
                    Vehicle = new Car
                    {
                        VehicleId = s.Vehicle.VehicleId,
                        LicensePlate = s.Vehicle.LicensePlate,
                        ClientId = s.Vehicle.ClientId
                    },
                    Technician = new Mechanic
                    {
                        TechnicianId = s.Technician.TechnicianId,
                        TechnicianName = s.Technician.TechnicianName
                    }
                })
                .ToListAsync();
            
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetById(int id)
        {
            var service = await _context.Services
                .Include(s => s.Vehicle)
                .Include(s => s.Technician)
                .Select(s => new Service
                {
                    RepairId = s.RepairId,
                    VehicleId = s.VehicleId,
                    TechnicianId = s.TechnicianId,
                    RepairDate = s.RepairDate,
                    EstimatedWorkDescription = s.EstimatedWorkDescription,
                    RepairDescription = s.RepairDescription,
                    Hours = s.Hours,
                    FinishDate = s.FinishDate,
                    RepairCost = s.RepairCost,
                    RepairStatus = s.RepairStatus,
                    Vehicle = new Car
                    {
                        VehicleId = s.Vehicle.VehicleId,
                        LicensePlate = s.Vehicle.LicensePlate,
                        ClientId = s.Vehicle.ClientId
                    },
                    Technician = new Mechanic
                    {
                        TechnicianId = s.Technician.TechnicianId,
                        TechnicianName = s.Technician.TechnicianName
                    }
                })
                .FirstOrDefaultAsync(s => s.RepairId == id);
            
            if (service == null)
                return NotFound();
            
            return Ok(service);
        }
    }
} 