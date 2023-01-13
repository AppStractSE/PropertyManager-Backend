namespace Api.Dto.Request.ChoreComment.v1;

public class PostChoreCommentRequestDto
{
    public string Message { get; set; }
    public string CustomerChoreId { get; set; }
    public string UserId { get; set; }
    public DateTime Time { get; set; } = DateTime.Now;
}