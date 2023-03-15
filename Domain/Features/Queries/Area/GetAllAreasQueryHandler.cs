using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Areas;

public class GetAllAreasQueryHandler : IRequestHandler<GetAllAreasQuery, IList<Area>>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetAllAreasQueryHandler(IAreaRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Area>> Handle(GetAllAreasQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("areas"))
        {
            return await _cache.GetAsync<IList<Area>>("areas");
        }

        var response = _mapper.Map<IList<Area>>(await _repo.GetAllAsync());
        await _cache.SetAsync("areas", response);
        return response;
    }
}