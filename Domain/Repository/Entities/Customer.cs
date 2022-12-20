namespace Domain.Repository.Entities;

public class Customer {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AreaId { get; set; }
    public string TeamId { get; set; }
}