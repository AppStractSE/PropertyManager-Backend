using MediatR;

namespace Domain.Features.Queries.Customers;

public class GetAllCustomersQuery : IRequest<IList<Domain.Customer>>
{

}