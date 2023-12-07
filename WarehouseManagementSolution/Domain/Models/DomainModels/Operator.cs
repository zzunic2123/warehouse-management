using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.DomainModels;

[Table("Operator")]
public class Operator
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", Order=1, TypeName="integer")]
    public int? Id { get; set; }

    [Column("Name")]
    public string Name { get; set; }
    
    [Column("TenantId")]
    public int TenantId { get; set; }
    
    [Column("ValidationId")]
    public int? ValidationId { get; set; }
    
    [ForeignKey("ValidationId")]
    public OperatorValidation? Validation { get; set; }
    
    [Column("WarehouseId")]
    public int? WarehouseId { get; set; }

    [Column("Deleted")]
    public bool Deleted { get; set; }
    
    [Column("Email")]
    public string Email { get; set; }
    
    [ForeignKey("WarehouseId")]
    public Warehouse? Warehouse { get; set; }
}