using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Dto.Response.ChoreStatus.v1;
public class ChoreStatusResponseDto
{
    public Guid Id { get; set; }
    public string CustomerChoreId { get; set; }
    [Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "Date"), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
    public DateTime CompletedDate { get; set; }
    public string DoneBy { get; set; }
}