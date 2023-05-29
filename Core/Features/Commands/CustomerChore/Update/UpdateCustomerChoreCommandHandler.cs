using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.CustomerChore;

public class UpdateCustomerChoreCommandHandler : IRequestHandler<UpdateCustomerChoreCommand, Domain.CustomerChore>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    public UpdateCustomerChoreCommandHandler(ICustomerChoreRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Domain.CustomerChore> Handle(UpdateCustomerChoreCommand request, CancellationToken cancellationToken)
    {
        var existingObject = await _repo.GetById(request.Id.ToString());

        if (existingObject == null)
        {
            throw new Exception("CustomerChore not found");
        }

        existingObject.PeriodicId = request.PeriodicId;
        existingObject.Frequency = request.Frequency;
        existingObject.Description = request.Description;

        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.CustomerChore>(existingObject));
        await _cache.RemoveAsync("CustomerChores:");
        await _cache.RemoveAsync($"CustomerChore:{request.Id}");
        return _mapper.Map<Domain.CustomerChore>(response);
    }
}