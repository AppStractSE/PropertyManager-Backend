namespace Domain.Repository.Entities;

public class Role : BaseEntity {
    public Guid Id { get; set; }
    public string RoleName { get; set; }
}