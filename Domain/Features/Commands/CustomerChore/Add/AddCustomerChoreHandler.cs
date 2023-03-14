using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class AddCustomerChoreCommandHandler : IRequestHandler<AddCustomerChoreCommand, Domain.CustomerChore>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public AddCustomerChoreCommandHandler(ICustomerChoreRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _redisCache = redisCache;
    }
    public async Task<Domain.CustomerChore> Handle(AddCustomerChoreCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.CustomerChore>(request));
        await _redisCache.RemoveAsync("CustomerChores:");
        return _mapper.Map<Domain.CustomerChore>(response);
    }
}