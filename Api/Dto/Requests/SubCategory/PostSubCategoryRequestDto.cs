namespace Api.Dto.Request.SubCategory.v1;

public class PostSubCategoryRequestDto
{
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
}
