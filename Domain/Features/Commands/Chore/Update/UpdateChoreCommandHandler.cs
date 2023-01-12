using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.Chore;

public class UpdateChoreCommandHandler : IRequestHandler<UpdateChoreCommand, Domain.Chore>
{
    private readonly IChoreRepository _repo;
    private readonly IMapper _mapper;
    public UpdateChoreCommandHandler(IChoreRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Chore> Handle(UpdateChoreCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.Chore>(request));
        return _mapper.Map<Domain.Chore>(response);
    }
}