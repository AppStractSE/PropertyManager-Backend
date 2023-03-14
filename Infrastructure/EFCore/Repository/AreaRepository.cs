using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class AreaRepository : BaseRepository<Area>, IAreaRepository
{
    public AreaRepository(PropertyManagerContext context) : base(context)
    {

    }
}