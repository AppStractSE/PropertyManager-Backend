using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class ChoreStatusRepository : BaseRepository<ChoreStatus>, IChoreStatusRepository
{
    public ChoreStatusRepository(PropertyManagerContext context) : base(context)
    {

    }
}