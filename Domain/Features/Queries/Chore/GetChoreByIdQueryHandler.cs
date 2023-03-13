using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Chores;

public class GetChoreByIdQueryHandler : IRequestHandler<GetChoreByIdQuery, Chore>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetChoreByIdQueryHandler(IChoreRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Chore> Handle(GetChoreByIdQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists($"Chore:{request.Id}"))
        {
            return await _cache.GetAsync<Chore>($"Chore:{request.Id}");
        }

        var mappedDomain = _mapper.Map<Chore>(await _repo.GetById(request.Id));
        await _cache.SetAsync($"Chore:{request.Id}", mappedDomain);
        return mappedDomain;
    }
}