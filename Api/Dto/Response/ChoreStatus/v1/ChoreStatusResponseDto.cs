using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Dto.Response.ChoreStatus.v1;
public class ChoreStatusResponseDto
{
    public Guid Id { get; set; }
    public string CustomerChoreId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletedDate { get; set; }
    public string DoneBy { get; set; }
}