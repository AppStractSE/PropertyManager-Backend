using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Chores;

public class GetAllChoresQueryHandler : IRequestHandler<GetAllChoresQuery, IList<Chore>>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    public GetAllChoresQueryHandler(IChoreRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<IList<Chore>> Handle(GetAllChoresQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("Chores:"))
        {
            return await _cache.GetAsync<IList<Chore>>("Chores:");
        }

        var mappedDomain = _mapper.Map<IList<Chore>>(await _repo.GetAllAsync());
        await _cache.SetAsync("Chores:", mappedDomain);
        return mappedDomain;
    }
}