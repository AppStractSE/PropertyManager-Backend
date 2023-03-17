using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Areas;

public class GetAreaByIdQueryHandler : IRequestHandler<GetAreaByIdQuery, Area>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetAreaByIdQueryHandler(IAreaRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Area> Handle(GetAreaByIdQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists($"Area:{request.Id}"))
        {
            return await _cache.GetAsync<Area>($"Area:{request.Id}");
        }

        var area = _mapper.Map<Area>(await _repo.GetById(request.Id));
        await _cache.SetAsync($"Area:{area.Id}", area);
        return area;
    }
}