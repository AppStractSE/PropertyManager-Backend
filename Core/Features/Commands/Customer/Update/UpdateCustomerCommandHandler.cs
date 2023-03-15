using Core.Features.Commands.Customer;
using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Customer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Domain.Customer>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;
    public UpdateCustomerCommandHandler(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<Domain.Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _repo.UpdateAsync(_mapper.Map<Repository.Entities.Customer>(request));
        return _mapper.Map<Domain.Customer>(response);
    }
}