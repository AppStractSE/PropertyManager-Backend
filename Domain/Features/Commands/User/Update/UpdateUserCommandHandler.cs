using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.User;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Domain.User>
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;
    public UpdateUserCommandHandler(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.User>(request));
        return _mapper.Map<Domain.User>(response);
    }
}