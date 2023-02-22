using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.TeamMember;

public class UpdateTeamMembersCommandHandler : IRequestHandler<UpdateTeamMembersCommand, IList<Domain.TeamMember>>
{
    private readonly ITeamMemberRepository _repo;
    private readonly IMapper _mapper;
    public UpdateTeamMembersCommandHandler(ITeamMemberRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Domain.TeamMember>> Handle(UpdateTeamMembersCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateRangeAsync(_mapper.Map<IList<Repository.Entities.TeamMember>>(request.TeamMembers));
        return _mapper.Map<IList<Domain.TeamMember>>(response);
    }
}