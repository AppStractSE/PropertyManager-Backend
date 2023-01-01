using Domain.Repository.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class TeamRepository : BaseRepository<Team>, ITeamRepository
{
    public TeamRepository(PropertyManagerContext context) : base(context)
    {

    }
}