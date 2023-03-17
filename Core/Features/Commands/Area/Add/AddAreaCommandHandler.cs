using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Area;

public class AddAreaCommandHandler : IRequestHandler<AddAreaCommand, Domain.Area>
{
    private readonly IAreaRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public AddAreaCommandHandler(IAreaRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Domain.Area> Handle(AddAreaCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync("Areas:");
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.Area>(request));
        return _mapper.Map<Domain.Area>(response);
    }
}