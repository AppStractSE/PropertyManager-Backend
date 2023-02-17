namespace Api.Dto.Request.Customer.v1;

public class PutCustomerRequestDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AreaId { get; set; }
    public string TeamId { get; set; }
    public string Address { get; set; }
}