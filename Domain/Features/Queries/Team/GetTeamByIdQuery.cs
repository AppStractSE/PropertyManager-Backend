using MediatR;

namespace Domain.Features.Queries.Teams;

public class GetTeamByIdQuery : IRequest<Domain.Team>
{
    public string Id { get; set; }
}