using Core.Features.Commands.Customer;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Customer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Domain.Customer>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    
    public UpdateCustomerCommandHandler(ICustomerRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Domain.Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.Customer>(request));
        await _cache.RemoveAsync("Customers:");
        await _cache.RemoveAsync($"Customer:{request.Id}");
        return _mapper.Map<Domain.Customer>(response);
    }
}