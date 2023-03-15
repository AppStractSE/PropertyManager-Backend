using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(PropertyManagerContext context) : base(context)
    {

    }
}