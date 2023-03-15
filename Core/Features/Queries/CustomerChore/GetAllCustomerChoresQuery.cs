using MediatR;

namespace Core.Features.Queries.CustomerChores;

public class GetAllCustomerChoresQuery : IRequest<IList<Domain.CustomerChore>>
{
}