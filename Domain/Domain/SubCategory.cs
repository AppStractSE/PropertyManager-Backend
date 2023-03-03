namespace Domain.Domain;

public class SubCategory
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
}