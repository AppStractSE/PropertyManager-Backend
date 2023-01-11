using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.CustomerChore;

public class UpdateCustomerChoreCommandHandler : IRequestHandler<UpdateCustomerChoreCommand, Domain.CustomerChore>
{
    private readonly ICustomerChoreRepository _repo;
    private readonly IMapper _mapper;
    public UpdateCustomerChoreCommandHandler(ICustomerChoreRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.CustomerChore> Handle(UpdateCustomerChoreCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.CustomerChore>(request));
        return _mapper.Map<Domain.CustomerChore>(response);
    }
}