namespace Domain.Domain;

public class Category
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IList<SubCategory> SubCategories { get; set; }
}