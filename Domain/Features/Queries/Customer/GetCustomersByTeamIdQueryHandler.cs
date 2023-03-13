using Domain.Domain;
using Domain.Features.Queries.Teams;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Customers;

public class GetCustomersByTeamIdQueryHandler : IRequestHandler<GetCustomersByTeamIdQuery, IList<Customer>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICache _cache;
    private readonly ICustomerRepository _repo;

    public GetCustomersByTeamIdQueryHandler(ICustomerRepository repo, IMapper mapper, IMediator mediator, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
        _cache = cache;
    }
    public async Task<IList<Domain.Customer>> Handle(GetCustomersByTeamIdQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists($"Team:Customers:{request.Id}"))
        {
            return await _cache.GetAsync<IList<Domain.Customer>>($"Customers:{request.Id}");
        }

        var customers = _mapper.Map<IList<Domain.Customer>>(await _repo.GetQuery(x => x.TeamId == request.Id));
        var teams = await _mediator.Send(new GetAllTeamsQuery());
        foreach (var customer in customers)
        {
            customer.TeamId = teams.FirstOrDefault(x => x.Id.ToString() == customer.TeamId).Id.ToString();
        }

        await _cache.SetAsync($"Team:Customers:{request.Id}", customers);
        return customers;
    }
}