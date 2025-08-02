using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaragePortal.Domain.Entities;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClientId { get; set; }

    [Required]
    public string ClientName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string ClientEmail { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string ClientPassword { get; set; } = null!;
    public List<int>? VehicleIds = new List<int>();
    public ICollection<Car>? Vehicles { get; set; } = new List<Car>();
}