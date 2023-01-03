using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.TeamMember;

public class AddTeamMemberCommandHandler : IRequestHandler<AddTeamMemberCommand, Domain.TeamMember>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    public AddTeamMemberCommandHandler(ITeamMemberRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.TeamMember> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.TeamMember>(request));
        return _mapper.Map<Domain.TeamMember>(response);
    }
}