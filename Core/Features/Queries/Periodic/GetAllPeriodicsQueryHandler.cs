using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Periodics;

public class GetAllPeriodicsQueryHandler : IRequestHandler<GetAllPeriodicsQuery, IList<Periodic>>
{
    private readonly IPeriodicRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetAllPeriodicsQueryHandler(IPeriodicRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IList<Periodic>> Handle(GetAllPeriodicsQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("Periodics:"))
        {
            return await _cache.GetAsync<IList<Periodic>>("Periodics:");
        }

        var mappedDomain = _mapper.Map<IList<Periodic>>(await _repo.GetAllAsync());
        await _cache.SetAsync("Periodics:", mappedDomain);
        return mappedDomain;
    }
}