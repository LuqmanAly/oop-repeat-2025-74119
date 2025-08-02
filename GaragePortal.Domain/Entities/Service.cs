using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaragePortal.Domain.Entities;

public class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RepairId { get; set; }

    public int VehicleId { get; set; }
    public Car Vehicle { get; set; } = null!;

    public int TechnicianId { get; set; }
    public Mechanic Technician { get; set; } = null!;
   


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
        int hoursRounded = (int)Math.Ceiling(Hours);
        RepairCost = hoursRounded * 75;
    }
}