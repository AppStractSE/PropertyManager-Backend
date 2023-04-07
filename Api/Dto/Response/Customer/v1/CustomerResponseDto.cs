namespace Api.Dto.Response.Customer.v1;

public class CustomerResponseDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AreaId { get; set; }
    public string TeamId { get; set; }
    public string Address { get; set; }
    public string Slug { get; set; }
}