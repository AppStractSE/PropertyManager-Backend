namespace Domain.Domain;

public class UserCustomerChoreData
{
    public string CustomerChoreId { get; set; }
    public Chore Chore { get; set; }
    public int Frequency { get; set; }
    public Periodic Periodic { get; set; }
}