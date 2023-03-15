using MediatR;

namespace Core.Features.Queries.Customers;

public class GetCustomersByTeamIdQuery : IRequest<IList<Domain.Customer>>
{
    public string Id { get; set; }
}