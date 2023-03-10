namespace Domain.Repository.Entities;

public abstract class BaseEntity
{
    public int RowVersion { get; set; }
    public DateTime RowCreated { get; set; }
    public DateTime RowModified { get; set; }

}