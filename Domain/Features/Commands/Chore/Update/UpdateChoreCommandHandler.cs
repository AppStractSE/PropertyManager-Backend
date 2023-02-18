using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.Chore;

public class UpdateChoreCommandHandler : IRequestHandler<UpdateChoreCommand, Domain.Chore>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;
    public UpdateChoreCommandHandler(IChoreRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Chore> Handle(UpdateChoreCommand request, CancellationToken cancellationToken)
    {
        await _redisCache.RemoveAsync("Chores:");
        await _redisCache.RemoveAsync($"Chore:{request.Id}");

        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.Chore>(request));
        return _mapper.Map<Domain.Chore>(response);
    }
}