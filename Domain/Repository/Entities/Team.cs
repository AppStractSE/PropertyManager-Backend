namespace Domain.Repository.Entities;

public class Team : BaseEntity 
{
    public Guid Id { get; set; }
    public string Name  { get; set; }
}