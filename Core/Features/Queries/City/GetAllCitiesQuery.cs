using MediatR;

namespace Core.Features.Queries.Cities;

public class GetAllCitiesQuery : IRequest<IList<Domain.City>>
{

}