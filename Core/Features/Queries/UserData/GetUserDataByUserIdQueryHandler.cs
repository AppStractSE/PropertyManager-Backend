using Core.Domain;
using Core.Features.Queries.Categories;
using Core.Features.Queries.CustomerChores;
using Core.Features.Queries.Customers;
using Core.Features.Queries.TeamMembers;
using Core.Features.Queries.Teams;
using Core.Utilities;
using MediatR;

namespace Core.Features.Queries.UserData;

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
        var categories = await _mediator.Send(new GetAllCategoriesQuery());
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
                IsTemporary = teamMembers.First(tm => tm.TeamId == team.Id.ToString()).IsTemporary,
                UserCustomersData = userCustomers
                    .Where(c => c.TeamId == team.Id.ToString())
                    .Select(c => new UserCustomerData()
                    {
                        CustomerId = c.Id.ToString(),
                        CustomerName = c.Name,
                        CustomerSlug = c.Slug,
                        CustomerAddress = c.Address,
                        AreaId = c.AreaId,
                        CustomerChores = userCustomerChores
                            .Where(cc => cc.CustomerId == c.Id.ToString())
                            .Select(cc => new UserCustomerChoreData()
                            {
                                CustomerChoreId = cc.Id.ToString(),
                                Chore = cc.Chore,
                                Status = cc.Status,
                                Progress = cc.Progress,
                                Frequency = cc.Frequency,
                                SubCategoryName = categories.Where(category => category.SubCategories.Any(subcategory => subcategory.Id.ToString() == cc.Chore.SubCategoryId)).Select(category => category.SubCategories.First(subcategory => subcategory.Id.ToString() == cc.Chore.SubCategoryId).Title).FirstOrDefault(),
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