using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GaragePortal.Domain;

namespace GaragePortal.Razor.Models;
public class ServiceDTO
{

    public int RepairId { get; set; }

    public int VehicleId { get; set; }
    public CarDTO  Vehicle { get; set; } = null!;

    public string LicensePlate { get; set; } = string.Empty;

    public int TechnicianId { get; set; }
    public MechanicDTO Technician { get; set; } = null!;
    public String TechnicianName { get; set; }


    [Required]
    public DateTime RepairDate { get; set; }

    public string? EstimatedWorkDescription { get; set; }

    public string? RepairDescription { get; set; }

    [Range(0, int.MaxValue)]
    public decimal Hours { get; set; }

    public DateTime? FinishDate { get; set; }

    public decimal? RepairCost { get; set; }

    public string RepairStatus { get; set; } = "Pending";



    public void CalculateCost()
    {
        // Round up hours to next integer
        int hoursRounded = (int)Math.Ceiling(Hours);
        RepairCost = hoursRounded * 75;
    }
}