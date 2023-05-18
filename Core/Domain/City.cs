namespace Core.Domain;

public class City
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<Area> Areas { get; set; }
}