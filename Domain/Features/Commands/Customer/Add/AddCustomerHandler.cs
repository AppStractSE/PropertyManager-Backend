using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Customer;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Domain.Customer>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;

    public AddCustomerCommandHandler(ICustomerRepository repo, IMapper mapper, IRedisCache redisCache)
    {
        _redisCache = redisCache;
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Customer> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.Customer>(request));
        await _redisCache.RemoveAsync("Customers:");
        return _mapper.Map<Domain.Customer>(response);
    }
}