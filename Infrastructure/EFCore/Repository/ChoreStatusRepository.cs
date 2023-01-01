using Domain.Repository.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class ChoreStatusRepository : BaseRepository<ChoreStatus>, IChoreStatusRepository
{
    public ChoreStatusRepository(PropertyManagerContext context) : base(context)
    {

    }
}