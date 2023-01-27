namespace Domain.Domain;

public class UserCustomerData {
    public string CustomerId { get; set; }
    public IList<UserCustomerChoreData> CustomerChores { get; set; }
}