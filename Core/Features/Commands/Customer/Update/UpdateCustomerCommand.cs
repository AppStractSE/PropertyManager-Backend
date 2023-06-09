using MediatR;

namespace Core.Features.Commands.Customer;

public class UpdateCustomerCommand : IRequest<Domain.Customer>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AreaId { get; set; }
    public string TeamId { get; set; }
    public string Address { get; set; }
}