using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class DeleteCustomerChoreCommandHandler : IRequestHandler<DeleteCustomerChoreCommand, Domain.CustomerChore>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public DeleteCustomerChoreCommandHandler(ICustomerChoreRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Domain.CustomerChore> Handle(DeleteCustomerChoreCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.DeleteAsync(_mapper.Map<Repository.Entities.CustomerChore>(request));
        await _cache.RemoveAsync("CustomerChores:");
        await _cache.RemoveAsync($"CustomerChore:{request.Id}");
        return _mapper.Map<Domain.CustomerChore>(response);
    }
}