namespace Core.Domain;

public class Category
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public bool IsParent { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
    public IList<Category> SubCategories { get; set; }
}