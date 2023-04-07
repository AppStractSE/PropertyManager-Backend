namespace Core.Domain;

public class UserCustomerData
{
    public string CustomerId { get; set; }

    public string CustomerName { get; set; }
    public string CustomerSlug { get; set; }
    public string AreaId { get; set; }

    public string CustomerAddress { get; set; }
    public IList<UserCustomerChoreData> CustomerChores { get; set; }
}