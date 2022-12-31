using Domain.Repository.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class AreaRepository : BaseRepository<Area>, IAreaRepository
{
    public AreaRepository(PropertyManagerContext context) : base(context)
    {

    }
}