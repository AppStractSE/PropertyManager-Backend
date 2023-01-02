using MediatR;

namespace Domain.Features.Queries.CustomerChores;

public class GetAllCustomerChoresQuery : IRequest<IList<Domain.CustomerChore>>
{
}