using MediatR;

namespace Domain.Features.Queries.Chores;

public class GetAllChoresQuery : IRequest<IList<Domain.Chore>>
{

}