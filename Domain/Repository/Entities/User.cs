namespace Domain.Repository.Entities;

public class User : BaseEntity {
    public Guid Id { get; set; }
    public string CredId { get; set; }
    public string RoleId { get; set; }
    public string Name { get; set; }
}