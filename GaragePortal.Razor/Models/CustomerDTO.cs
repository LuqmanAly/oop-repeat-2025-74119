using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaragePortal.Razor.Models;
public class CustomerDTO
{
    public int ClientId { get; set; }

    
    public string ClientName { get; set; } = null!;

    public string ClientEmail { get; set; } = null!;

  
    public string ClientPassword { get; set; } = null!;
    public List<int>? VehicleIds = new List<int>();
    public ICollection<CarDTO>? Vehicles { get; set; } = new List<CarDTO>();
}