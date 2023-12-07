using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.DomainModels;

[Table("Address")]
public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", Order=1, TypeName="integer")]
    public int? Id { get; set; }
    
    [Column("Street")]
    public string Street { get; set; }
    
    [Column("City")]
    public string City { get; set; }
    
    [Column("ZipCode")]
    public string ZipCode { get; set; }
    
    [Column("WarehouseId")]
    public int? WarehouseId { get; set; }
    
    [Column("Deleted")]
    public bool Deleted { get; set; }
    
   // public Warehouse? Warehouse { get; set; } = null!;
}