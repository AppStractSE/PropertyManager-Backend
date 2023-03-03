using MediatR;

namespace Core.Features.Queries.Customers;

public class GetAllCustomersQuery : IRequest<IList<Domain.Customer>>
{

}