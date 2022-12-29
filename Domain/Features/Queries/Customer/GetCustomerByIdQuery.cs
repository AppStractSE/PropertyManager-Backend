using MediatR;

namespace Domain.Features.Queries.Customers;

public class GetCustomerByIdQuery : IRequest<Domain.Customer>
{
    public string Id { get; set; }
}