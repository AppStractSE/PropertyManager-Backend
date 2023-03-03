namespace Api.Dto.Response.SubCategory.v1;
public class SubCategoryResponseDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
}