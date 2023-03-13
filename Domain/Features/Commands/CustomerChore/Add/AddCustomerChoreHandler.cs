using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.CustomerChore;

public class AddCustomerChoreCommandHandler : IRequestHandler<AddCustomerChoreCommand, Domain.CustomerChore>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public AddCustomerChoreCommandHandler(ICustomerChoreRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Domain.CustomerChore> Handle(AddCustomerChoreCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.CustomerChore>(request));
        await _cache.RemoveAsync("CustomerChores:");
        return _mapper.Map<Domain.CustomerChore>(response);
    }
}