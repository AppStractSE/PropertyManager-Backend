using MediatR;

namespace Core.Features.Queries.ChoreComments;

public class GetLatestFiveChoreCommentsQuery : IRequest<IList<Domain.ChoreComment>>
{

}