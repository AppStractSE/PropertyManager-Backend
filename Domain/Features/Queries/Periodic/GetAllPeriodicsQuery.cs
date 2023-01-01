using MediatR;

namespace Domain.Features.Queries.Periodics;

public class GetAllPeriodicsQuery : IRequest<IList<Domain.Periodic>>
{

}