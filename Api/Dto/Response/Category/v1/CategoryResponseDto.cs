using Api.Dto.Response.SubCategory.v1;

namespace Api.Dto.Response.Category.v1;
public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IList<SubCategoryResponseDto> SubCategories { get; set; }
}