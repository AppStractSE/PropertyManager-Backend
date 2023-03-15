using MediatR;

namespace Core.Features.Queries.Teams;

public class GetAllTeamsQuery : IRequest<IList<Domain.Team>>
{

}