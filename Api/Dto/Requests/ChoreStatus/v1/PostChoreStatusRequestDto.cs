namespace Api.Dto.Request.ChoreStatus.v1;

public class PostChoreStatusRequestDto
{
    public string? CustomerChoreId { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string? DoneBy { get; set; }
}