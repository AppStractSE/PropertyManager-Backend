using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class PeriodicRepository : BaseRepository<Periodic>, IPeriodicRepository
{
    public PeriodicRepository(PropertyManagerContext context) : base(context)
    {

    }
}