using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class CustomerChoreRepository : BaseRepository<CustomerChore>, ICustomerChoreRepository
{
    public CustomerChoreRepository(PropertyManagerContext context) : base(context)
    {

    }
}