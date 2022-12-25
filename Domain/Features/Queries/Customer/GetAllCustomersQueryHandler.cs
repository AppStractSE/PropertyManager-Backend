using Domain.Domain;
using Domain.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Domain.Features.Queries.Customers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IList<Customer>>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;
    public GetAllCustomersQueryHandler(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IList<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IList<Customer>>(await _repo.GetAllAsync());
    }
}