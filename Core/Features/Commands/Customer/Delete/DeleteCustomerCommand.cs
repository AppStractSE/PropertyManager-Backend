using MediatR;

namespace Core.Features.Commands.Customer;

public class DeleteCustomerCommand : IRequest<Domain.Customer>
{
    public Guid Id { get; set; }
}