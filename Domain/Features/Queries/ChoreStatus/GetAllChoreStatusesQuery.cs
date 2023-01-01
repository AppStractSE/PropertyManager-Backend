using MediatR;

namespace Domain.Features.Queries.ChoreStatuses;

public class GetAllChoreStatusesQuery : IRequest<IList<Domain.ChoreStatus>>
{

}