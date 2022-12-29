using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Repository.Entities;

public class ChoreStatus : BaseEntity {
    public Guid Id { get; set; }
    public string CustomerChoreId { get; set; }
    [Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime CompletedDate { get; set; }
    public int DoneBy { get; set; }
}