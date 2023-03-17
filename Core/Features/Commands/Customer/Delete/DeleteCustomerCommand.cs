using MediatR;

namespace Core.Features.Commands.Customer;

public class DeleteCustomerCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}