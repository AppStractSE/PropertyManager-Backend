using MediatR;

namespace Core.Features.Queries.Chores;

public class GetAllChoresQuery : IRequest<IList<Domain.Chore>>
{

}