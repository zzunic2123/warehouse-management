using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.DomainModels;

public class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id", Order=1, TypeName="integer")]
    public int? Id { get; set; }
    
    [Column("CreatedAt")]
    public DateTimeOffset CreatedAt { get; set; }
    
    [Column("UpdatedAt")]
    public DateTimeOffset UpdatedAt { get; set; }
    
    [Column("ReservedByOperator")]
    public int ReservedByOperator { get; set; }
    
    [Column("ReservationDate")]
    public DateTimeOffset? ReservationDate { get; set; }
    
    [Column("ExpirationDate")]
    public DateTimeOffset ExpirationDate { get; set; }
    
    [Column("Deleted")]
    public bool Deleted { get; set; }
    
    }