using MediatR;

namespace Core.Features.Queries.CustomerChores;

public class GetCustomerChoresByCustomerIdQuery : IRequest<IList<Domain.CustomerChore>>
{
    public string Id { get; set; }
}