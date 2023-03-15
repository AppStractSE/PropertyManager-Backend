namespace Core.Repository.Entities;

public class Category : BaseEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
