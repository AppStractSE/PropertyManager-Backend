using Domain.Domain;
using Domain.Repository.Interfaces;
using MediatR;

namespace Domain.Features.Queries.Customers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IList<Customer>>
{
    private readonly ICustomerRepository _repo;
    public GetAllCustomersQueryHandler(ICustomerRepository repo)
    {
        _repo = repo;
    }
    public Task<IList<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}