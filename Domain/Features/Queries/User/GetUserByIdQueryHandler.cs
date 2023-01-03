using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Users;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;
    public GetUserByIdQueryHandler(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<User>(await _repo.GetById(request.Id));
    }
}