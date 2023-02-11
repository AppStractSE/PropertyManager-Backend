using MediatR;

namespace Domain.Features.Queries.ChoreComments;

public class GetLatestFiveChoreCommentsQuery : IRequest<IList<Domain.ChoreComment>>
{

}