using Domain.Domain;
using Domain.Features.Queries.Teams;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Features.Queries.Customers;

public class GetCustomersByTeamIdQueryHandler : IRequestHandler<GetCustomersByTeamIdQuery, IList<Customer>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    private readonly ICustomerRepository _repo;

    public GetCustomersByTeamIdQueryHandler(ICustomerRepository repo, IMapper mapper, IMediator mediator)
    {
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<IList<Domain.Customer>> Handle(GetCustomersByTeamIdQuery request, CancellationToken cancellationToken)
    {
        var customers = _mapper.Map<IList<Domain.Customer>>(await _repo.GetQuery(x => x.TeamId == request.Id));
        var teams = await _mediator.Send(new GetAllTeamsQuery());
        foreach (var customer in customers)
        {
            customer.TeamId = teams.FirstOrDefault(x => x.Id.ToString() == customer.TeamId).Id.ToString();
        }
        return customers;
    }
}