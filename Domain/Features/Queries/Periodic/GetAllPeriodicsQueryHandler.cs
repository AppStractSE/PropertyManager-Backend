using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Periodics;

public class GetAllPeriodicsQueryHandler : IRequestHandler<GetAllPeriodicsQuery, IList<Periodic>>
{
    private readonly IPeriodicRepository _repo;
    private readonly IMapper _mapper;
    public GetAllPeriodicsQueryHandler(IPeriodicRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Periodic>> Handle(GetAllPeriodicsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<Periodic>>(await _repo.GetAllAsync());
    }
}