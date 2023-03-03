using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Customers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IList<Customer>>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public GetAllCustomersQueryHandler(ICustomerRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _repo = repo;
        _mapper = mapper;
        _redisCache = redisCache;
    }

    public async Task<IList<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists("Customers:"))
        {
            return await _redisCache.GetAsync<IList<Customer>>("Customers:");
        }
        
        var mappedDomain = _mapper.Map<IList<Customer>>(await _repo.GetAllAsync());
        await _redisCache.SetAsync("Customers:", mappedDomain);
        return mappedDomain;
    }
}