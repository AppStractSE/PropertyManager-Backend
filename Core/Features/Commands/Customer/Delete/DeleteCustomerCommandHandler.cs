using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Customer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Domain.Customer>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public DeleteCustomerCommandHandler(ICustomerRepository repo, IMapper mapper, ICache cache)
    {
        _cache = cache;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Domain.Customer> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        // Deletea alla customer chores
        var response = await _repo.DeleteAsync(_mapper.Map<Repository.Entities.Customer>(request));
        await _cache.RemoveAsync("Customers:");
        await _cache.RemoveAsync($"Customer:{request.Id}");
        return _mapper.Map<Domain.Customer>(response);
    }
}