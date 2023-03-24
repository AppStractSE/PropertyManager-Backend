using MediatR;

namespace Core.Features.Commands.Customer;

public class DeleteCustomerCommand : IRequest<bool>
{
    public Guid CustomerId { get; set; }
}