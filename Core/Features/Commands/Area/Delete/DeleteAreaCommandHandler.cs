using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Area;

public class DeleteAreaCommandHandler : IRequestHandler<DeleteAreaCommand, Domain.Area>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public DeleteAreaCommandHandler(IAreaRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Domain.Area> Handle(DeleteAreaCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.DeleteAsync(_mapper.Map<Repository.Entities.Area>(request));
        await _cache.RemoveAsync("Areas:");
        await _cache.RemoveAsync($"Area:{request.Id}");
        return _mapper.Map<Domain.Area>(response);
    }
}