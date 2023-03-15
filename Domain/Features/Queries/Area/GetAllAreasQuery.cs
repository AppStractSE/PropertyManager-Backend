using MediatR;

namespace Core.Features.Queries.Areas;

public class GetAllAreasQuery : IRequest<IList<Domain.Area>>
{

}