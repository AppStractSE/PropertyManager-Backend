namespace Core.Repository.Entities;

public class Category : BaseEntity
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
}
