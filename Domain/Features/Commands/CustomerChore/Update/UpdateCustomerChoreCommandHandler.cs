using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.CustomerChore;

public class UpdateCustomerChoreCommandHandler : IRequestHandler<UpdateCustomerChoreCommand, Domain.CustomerChore>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;
    public UpdateCustomerChoreCommandHandler(ICustomerChoreRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _redisCache = redisCache;
    }
    public async Task<Domain.CustomerChore> Handle(UpdateCustomerChoreCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.CustomerChore>(request));
        await _redisCache.RemoveAsync("CustomerChores:");
        await _redisCache.RemoveAsync($"CustomerChore:{request.Id}");
        return _mapper.Map<Domain.CustomerChore>(response);
    }
}