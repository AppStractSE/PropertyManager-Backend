namespace Api.Dto.Response.Chore.v1;
public class ChoreResponseDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
}