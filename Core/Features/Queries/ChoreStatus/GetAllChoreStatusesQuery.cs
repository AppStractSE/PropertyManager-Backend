using MediatR;

namespace Core.Features.Queries.ChoreStatuses;

public class GetAllChoreStatusesQuery : IRequest<IList<Domain.ChoreStatus>>
{

}