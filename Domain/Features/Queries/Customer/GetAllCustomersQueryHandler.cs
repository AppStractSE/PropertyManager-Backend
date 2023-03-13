using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Customers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IList<Customer>>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetAllCustomersQueryHandler(ICustomerRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IList<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists("Customers:"))
        {
            return await _cache.GetAsync<IList<Customer>>("Customers:");
        }

        var mappedDomain = _mapper.Map<IList<Customer>>(await _repo.GetAllAsync());
        await _cache.SetAsync("Customers:", mappedDomain);
        return mappedDomain;
    }
}