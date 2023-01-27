using Domain.Domain;
using Domain.Features.Queries.Chores;
using Domain.Features.Queries.CustomerChores;
using Domain.Features.Queries.Customers;
using Domain.Features.Queries.Periodics;
using Domain.Features.Queries.TeamMembers;
using Domain.Features.Queries.Teams;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.UserData;

public class GetUserDataByUserIdQueryHandler : IRequestHandler<GetUserDataByUserIdQuery, TeamMember>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public GetUserDataByUserIdQueryHandler(IMapper mapper, Mediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<TeamMember> Handle(GetUserDataByUserIdQuery request, CancellationToken cancellationToken)
    {
        var teamMembers = await _mediator.Send(new GetTeamMembersByUserIdQuery() { Id = request.Id });

        var teams = await _mediator.Send(new GetAllTeamsQuery());

        var customers = await _mediator.Send(new GetAllCustomersQuery());

        var customerChores = await _mediator.Send(new GetAllCustomerChoresQuery());

        var chores = await _mediator.Send(new GetAllChoresQuery());

        var periodic = await _mediator.Send(new GetAllPeriodicsQuery());

        var userData = new Domain.UserData()
        {

        };

        return _mapper.Map<TeamMember>(await _repo.GetById(request.Id));
    }
}