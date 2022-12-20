using Domain.Repository.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class CustomerRepository : BaseRepository<Customer>
{
    public CustomerRepository(DBContext context) : base(context)
    {

    }
}