namespace Api.Dto.Request.Category.v1;

public class PostCategoryRequestDto
{
    public string ParentId { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
}