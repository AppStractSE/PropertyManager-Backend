using Core.Features.Commands.TeamMember;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Team;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, bool>
{
    private readonly ITeamRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
     private readonly IMediator _mediator;

    public DeleteTeamCommandHandler(ITeamRepository repo, IMapper mapper, ICache cache, IMediator mediator)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
         _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var result = false;
        var teamToDelete = await _repo.GetById(request.Id.ToString());

        if (teamToDelete != null) 
        {
            result = await _repo.DeleteAsync(teamToDelete);
        }

        if (result) 
        {
            await _mediator.Send(new BulkDeleteTeamMembersCommand { TeamId = request.Id.ToString() }, cancellationToken);
            await _cache.RemoveAsync("Teams:");
            await _cache.RemoveAsync($"Team:{request.Id}");
        }
       
        return result;
    }
}