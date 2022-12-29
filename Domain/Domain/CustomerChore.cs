namespace Domain.Domain;

public class CustomerChore
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ChoreId { get; set; }
    public int Frequency { get; set; }
    public Guid PeriodicId { get; set; }
}