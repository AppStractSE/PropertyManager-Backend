using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class DeleteCustomerChoreCommand : IRequest<Domain.CustomerChore>
{
    public Guid Id { get; set; }
}