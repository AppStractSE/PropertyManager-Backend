using Api.Dto.Response.SubCategory.v1;

namespace Api.Dto.Response.Category.v1;
public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public bool IsParent { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
    public IList<CategoryResponseDto> SubCategories { get; set; }
}