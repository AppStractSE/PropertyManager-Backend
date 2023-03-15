using MediatR;

namespace Core.Features.Queries.ChoreComments;

public class GetAllChoreCommentsQuery : IRequest<IList<Domain.ChoreComment>>
{

}