using MediatR;

namespace Domain.Features.Queries.ChoreStatuses;

public class GetChoreStatusByIdQuery : IRequest<Domain.ChoreStatus>
{
    public string Id { get; set; }
}