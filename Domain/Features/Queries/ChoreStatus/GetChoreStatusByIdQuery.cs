using MediatR;

namespace Core.Features.Queries.ChoreStatuses;

public class GetChoreStatusByIdQuery : IRequest<IList<Domain.ChoreStatus>>
{
    public string Id { get; set; }
}