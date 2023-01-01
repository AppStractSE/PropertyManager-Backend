using MediatR;

namespace Domain.Features.Queries.Teams;

public class GetAllTeamsQuery : IRequest<IList<Domain.Team>>
{

}