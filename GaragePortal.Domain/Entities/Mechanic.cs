using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaragePortal.Domain.Entities;

public class Mechanic
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TechnicianId { get; set; }

    [Required]
    public string TechnicianName { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string TechnicianPassword { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string TechnicianEmail { get; set; } = null!;

    public ICollection<Service> Repairs { get; set; } = new List<Service>();
}
