namespace Core.Repository.Entities;

public class Area : BaseEntity
{
    public Guid Id { get; set; }
    public string CityId { get; set; }
    public string Name { get; set; }
}
