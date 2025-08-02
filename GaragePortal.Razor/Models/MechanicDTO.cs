using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaragePortal.Razor.Models;
public class MechanicDTO
{
    public int TechnicianId { get; set; }


    public string TechnicianName { get; set; } 

    
    public string TechnicianPassword { get; set; } = null!;
    
    public string TechnicianEmail { get; set; } = null!;

    public ICollection<ServiceDTO> Repairs { get; set; } = new List<ServiceDTO>();
}
