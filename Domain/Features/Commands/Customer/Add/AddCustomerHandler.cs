using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Commands.Customer;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Domain.Customer>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;
    public AddCustomerCommandHandler(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Customer> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.Customer>(request));
        return _mapper.Map<Domain.Customer>(response);
    }
}