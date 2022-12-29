using MediatR;

namespace Domain.Features.Queries.CustomerChore;

public class GetCustomerChoresByCustomerIdQuery : IRequest<IList<Domain.CustomerChore>>
{
    public string Id { get; set; }
}