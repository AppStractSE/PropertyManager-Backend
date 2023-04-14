using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class ArchivedChoreStatusRepository : BaseRepository<ArchivedChoreStatus>, IArchivedChoreStatusRepository
{
    public ArchivedChoreStatusRepository(PropertyManagerContext context) : base(context)
    {

    }
}