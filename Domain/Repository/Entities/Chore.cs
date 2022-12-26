namespace Domain.Repository.Entities;

public class Chore : BaseEntity
{
    public Guid Id { get; set; }
    public string CategoryId { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
}