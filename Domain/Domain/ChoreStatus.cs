using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Domain;

public class ChoreStatus
{
    public Guid Id { get; set; }
    public string CustomerChoreId { get; set; }
    [Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
    public DateTime CompletedDate { get; set; }
    public string DoneBy { get; set; }
}