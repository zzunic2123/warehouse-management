using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.DomainModels;
    
[Table("OperatorValidation")]
public class OperatorValidation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", Order=1, TypeName="integer")]
    public int Id { get; set; }
    
    [Column("OperatorId")]
    public int OperatorId { get; set; }
    
    [Column("BackOfficeUserId")]
    public int BackOfficeUserId { get; set; }
    
    [Column("CreatedAt")]
    public DateTimeOffset CreatedAt { get; set; }
    
    [Column("WarehouseId")]
    public int? WarehouseId { get; set; }
}