using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaragePortal.Domain.Entities;

public class Car
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VehicleId { get; set; }

    [Required]
    public string LicensePlate { get; set; } = null!;

    public int ClientId { get; set; }
    public Customer? Client { get; set; } = null!;

    public ICollection<Service>? Repairs { get; set; } = new List<Service>();
}
