using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Chore;

public class UpdateChoreCommandHandler : IRequestHandler<UpdateChoreCommand, Domain.Chore>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    public UpdateChoreCommandHandler(IChoreRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Chore> Handle(UpdateChoreCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync("Chores:");
        await _cache.RemoveAsync($"Chore:{request.Id}");

        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.Chore>(request));
        return _mapper.Map<Domain.Chore>(response);
    }
}