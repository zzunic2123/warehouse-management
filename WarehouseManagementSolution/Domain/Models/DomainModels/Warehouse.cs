using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.DomainModels;

[Table("Warehouse")]
public class Warehouse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", Order=1, TypeName="integer")]
    public int? Id { get; set; }
    
    [Column("Name")]
    public string Name { get; set; }
    
    
    [Column("Capacity")]
    public int Capacity { get; set; }
    
    [Column("OperatorId")]
    public int? OperatorId { get; set; }

    [ForeignKey("OperatorId")]
    public Operator? Operator { get; set; }
    
    [Column("AddressId")]
    public int? AddressId { get; set; }
    
    [ForeignKey("AddressId")]
    public Address? Address { get; set; }
    
}