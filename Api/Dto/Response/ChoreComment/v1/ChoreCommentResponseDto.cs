namespace Api.Dto.Response.ChoreComment.v1;
public class ChoreCommentResponseDto
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime Time { get; set; }
    public string DisplayName { get; set; }
    public string UserId { get; set; }
}