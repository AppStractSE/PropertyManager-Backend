using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Customers;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public GetCustomerByIdQueryHandler(ICustomerRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        if (_redisCache.Exists($"Customer:{request.Id}"))
        {
            return await _redisCache.GetAsync<Customer>($"Customer:{request.Id}");
        }
        
        var mappedDomain = _mapper.Map<Customer>(await _repo.GetById(request.Id));
        await _redisCache.SetAsync($"Customer:{request.Id}", mappedDomain);
        return mappedDomain;
    }
}