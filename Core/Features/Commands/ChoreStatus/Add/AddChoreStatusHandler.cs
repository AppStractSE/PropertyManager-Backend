using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.ChoreStatus;

public class AddChoreStatusCommandHandler : IRequestHandler<AddChoreStatusCommand, Domain.ChoreStatus>
{
    private readonly IChoreStatusRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public AddChoreStatusCommandHandler(IChoreStatusRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Domain.ChoreStatus> Handle(AddChoreStatusCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.ChoreStatus>(request));
        response.StartDate = DateTime.Now;
        response.CompletedDate = DateTime.Now;
        await _cache.RemoveAsync($"ChoreStatuses:{request.CustomerChoreId}");
        return _mapper.Map<Domain.ChoreStatus>(response);
    }
}