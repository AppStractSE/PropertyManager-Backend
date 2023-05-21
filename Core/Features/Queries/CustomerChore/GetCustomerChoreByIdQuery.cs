using MediatR;

namespace Core.Features.Queries.CustomerChores;

public class GetCustomerChoreByIdQuery : IRequest<Domain.CustomerChore>
{
    public string Id { get; set; }
}