using Core.Domain;
using Core.Features.Queries.Teams;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Customers;

public class GetCustomersByTeamIdQueryHandler : IRequestHandler<GetCustomersByTeamIdQuery, IList<Customer>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IRedisCache _redisCache;
    private readonly ICustomerRepository _repo;

    public GetCustomersByTeamIdQueryHandler(ICustomerRepository repo, IMapper mapper, IMediator mediator, IRedisCache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _mediator = mediator;
        _redisCache = redisCache;
    }
    public async Task<IList<Domain.Customer>> Handle(GetCustomersByTeamIdQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists($"Team:Customers:{request.Id}"))
        {
            return await _redisCache.GetAsync<IList<Domain.Customer>>($"Customers:{request.Id}");
        }
        
        var customers = _mapper.Map<IList<Domain.Customer>>(await _repo.GetQuery(x => x.TeamId == request.Id));
        var teams = await _mediator.Send(new GetAllTeamsQuery());
        foreach (var customer in customers)
        {
            customer.TeamId = teams.FirstOrDefault(x => x.Id.ToString() == customer.TeamId).Id.ToString();
        }
        
        await _redisCache.SetAsync($"Team:Customers:{request.Id}", customers);
        return customers;
    }
}