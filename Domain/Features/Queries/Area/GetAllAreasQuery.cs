using MediatR;

namespace Domain.Features.Queries.Areas;

public class GetAllAreasQuery : IRequest<IList<Domain.Area>>
{

}