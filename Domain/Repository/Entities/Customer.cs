namespace Core.Repository.Entities;

public class Customer : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AreaId { get; set; }
    public string TeamId { get; set; }
    public string Address { get; set; }
}