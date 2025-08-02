using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaragePortal.Razor.Models
{
    public class CarDTO
    {
        
        public int VehicleId { get; set; }

        
        public string LicensePlate { get; set; } = null!;

        public int ClientId { get; set; }
        public CustomerDTO? Client { get; set; } = null!;

        // Navigation property
        public ICollection<ServiceDTO>? Repairs { get; set; } = new List<ServiceDTO>();
    }
}
