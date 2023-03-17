using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Area;

public class UpdateAreaCommandHandler : IRequestHandler<UpdateAreaCommand, Domain.Area>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    public UpdateAreaCommandHandler(IAreaRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Domain.Area> Handle(UpdateAreaCommand request, CancellationToken cancellationToken)
    {

        await _cache.RemoveAsync("Areas:");
        await _cache.RemoveAsync($"Area:{request.Id}");

        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.Area>(request));
        return _mapper.Map<Domain.Area>(response);
    }
}