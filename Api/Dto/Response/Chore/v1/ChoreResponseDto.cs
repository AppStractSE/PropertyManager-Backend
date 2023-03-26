namespace Api.Dto.Response.Chore.v1;
public class ChoreResponseDto
{
    public Guid Id { get; set; }
    public Guid SubCategoryId { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
}