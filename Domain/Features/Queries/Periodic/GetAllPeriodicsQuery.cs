using MediatR;

namespace Core.Features.Queries.Periodics;

public class GetAllPeriodicsQuery : IRequest<IList<Domain.Periodic>>
{

}