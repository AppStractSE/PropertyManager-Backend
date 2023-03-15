using Core.Domain;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Customers;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public GetCustomerByIdQueryHandler(ICustomerRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        if (_cache.Exists($"Customer:{request.Id}"))
        {
            return await _cache.GetAsync<Customer>($"Customer:{request.Id}");
        }

        var mappedDomain = _mapper.Map<Customer>(await _repo.GetById(request.Id));
        await _cache.SetAsync($"Customer:{request.Id}", mappedDomain);
        return mappedDomain;
    }
}