namespace Core.Domain;

public class UserCustomerChoreData
{
    public string CustomerChoreId { get; set; }
    public Chore Chore { get; set; }
    public int Frequency { get; set; }
    public string Status { get; set; }
    public int Progress { get; set; }
    public string SubCategoryName { get; set; }
    public Periodic Periodic { get; set; }
}