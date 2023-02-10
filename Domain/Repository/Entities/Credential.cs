namespace Domain.Repository.Entities;

public class Credential : BaseEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}