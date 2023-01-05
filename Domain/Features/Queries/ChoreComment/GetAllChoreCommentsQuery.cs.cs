using MediatR;

namespace Domain.Features.Queries.ChoreComments;

public class GetAllChoreCommentsQuery : IRequest<IList<Domain.ChoreComment>>
{

}