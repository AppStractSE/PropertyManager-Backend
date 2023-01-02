namespace Domain.Domain;

public class CustomerChore
{
    public Guid Id { get; set; }
    public string CustomerId { get; set; }
    public string ChoreId { get; set; }
    public int Frequency { get; set; }
    public string PeriodicId { get; set; }
}