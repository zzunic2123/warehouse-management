using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.DomainModels;

[Table("packages")]
public class Package
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    
    [Column("Name")]
    public string Name { get; set; }
    
    [Column("Description")]
    public string Description { get; set; }
    
    [Column("Quantity")]
    public int Quantity { get; set; }
    
    [Column("ReservationId")]
    public int? ReservationId { get; set; }
    
    [ForeignKey("ReservationId")]
    public Reservation? Reserved { get; set; }
    
}