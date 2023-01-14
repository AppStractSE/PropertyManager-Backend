namespace Api.Dto.Request.ChoreStatus.v1;

public class PostChoreStatusRequestDto
{
    public Guid Id { get; set; }
    public string? CustomerChoreId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime CompletedDate { get; set; } = DateTime.Now;
    public string? DoneBy { get; set; }
}