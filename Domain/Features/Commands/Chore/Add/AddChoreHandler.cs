using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.Chore;

public class AddChoreCommandHandler : IRequestHandler<AddChoreCommand, Domain.Chore>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    public AddChoreCommandHandler(IChoreRepository repo, IMapper mapper, ICache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = redisCache;
    }
    public async Task<Domain.Chore> Handle(AddChoreCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync("Chores:");
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.Chore>(request));
        return _mapper.Map<Domain.Chore>(response);
    }
}