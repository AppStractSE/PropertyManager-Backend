namespace Domain.Domain;

public class Chore 
{
    public Guid Id { get; set; }
    public string CategoryId { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
}