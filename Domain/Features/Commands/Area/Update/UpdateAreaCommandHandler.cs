using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.Area;

public class UpdateAreaCommandHandler : IRequestHandler<UpdateAreaCommand, Domain.Area>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;
    public UpdateAreaCommandHandler(IAreaRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<Domain.Area> Handle(UpdateAreaCommand request, CancellationToken cancellationToken)
    {

       await _redisCache.RemoveAsync("areas");
       await _redisCache.RemoveAsync($"area_{request.Id}");

       var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.Area>(request));
       return _mapper.Map<Domain.Area>(response);
    }
}