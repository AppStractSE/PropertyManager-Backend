namespace Api.Dto.Request.ChoreStatus.v1;

public class PostChoreStatusRequestDto
{
    public string Message { get; set; }
    public string CustomerChoreId { get; set; }
    public string UserId { get; set; }
    public DateTime Time { get; set; } = DateTime.Now;
}