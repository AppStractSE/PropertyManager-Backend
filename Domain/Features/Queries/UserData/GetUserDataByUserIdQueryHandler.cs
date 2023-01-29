using Domain.Domain;
using Domain.Features.Queries.CustomerChores;
using Domain.Features.Queries.Customers;
using Domain.Features.Queries.TeamMembers;
using Domain.Features.Queries.Teams;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.UserData;

public class GetUserDataByUserIdQueryHandler : IRequestHandler<GetUserDataByUserIdQuery, Domain.UserData>
{
    private readonly IMediator _mediator;

    public GetUserDataByUserIdQueryHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<Domain.UserData> Handle(GetUserDataByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await BuildUserData(request);
    }

    private async Task<Domain.UserData> BuildUserData(GetUserDataByUserIdQuery request)
    {
        var teamMembers = await _mediator.Send(new GetTeamMembersByUserIdQuery() { Id = request.Id });
        var teams = await _mediator.Send(new GetAllTeamsQuery());
        var customers = await _mediator.Send(new GetAllCustomersQuery());
        var customerChores = await _mediator.Send(new GetAllCustomerChoresQuery());

        var userData = new Domain.UserData();
        var userTeams = new List<UserTeamData>();

        var userTeamIds = teamMembers.Select(tm => tm.TeamId);
        var userCustomers = customers.Where(c => userTeamIds.Contains(c.TeamId));
        var userCustomerChores = customerChores.Where(cc => userCustomers.Select(c => c.Id.ToString()).Contains(cc.CustomerId));

        foreach (var team in teams.Where(t => userTeamIds.Contains(t.Id.ToString())))
        {
            var userTeam = new UserTeamData()
            {
                TeamId = team.Id.ToString(),
                TeamName = team.Name,
                UserCustomersData = userCustomers
                    .Where(c => c.TeamId == team.Id.ToString())
                    .Select(c => new UserCustomerData()
                    {
                        CustomerId = c.Id.ToString(),
                        CustomerName = c.Name,
                        CustomerAddress = c.Address,
                        AreaId = c.AreaId,
                        CustomerChores = userCustomerChores
                            .Where(cc => cc.CustomerId == c.Id.ToString())
                            .Select(cc => new UserCustomerChoreData()
                            {
                                CustomerChoreId = cc.Id.ToString(),
                                Chore = cc.Chore,
                                Frequency = cc.Frequency,
                                Periodic = cc.Periodic
                            }).ToList()
                    }).ToList()
            };
            userTeams.Add(userTeam);
        }
        userData.UserTeamsData = userTeams;
        return userData;
    }

}