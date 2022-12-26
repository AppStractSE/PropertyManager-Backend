using Domain.Repository.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class ChoreRepository : BaseRepository<Chore>, IChoreRepository
{
    public ChoreRepository(PropertyManagerContext context) : base(context)
    {

    }
}