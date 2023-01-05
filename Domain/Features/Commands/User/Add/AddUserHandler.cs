using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.User;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Domain.User>
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;
    public AddUserCommandHandler(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.User>(request));
        return _mapper.Map<Domain.User>(response);
    }
}