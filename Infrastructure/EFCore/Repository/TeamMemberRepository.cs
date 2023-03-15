using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class TeamMemberRepository : BaseRepository<TeamMember>, ITeamMemberRepository
{
    public TeamMemberRepository(PropertyManagerContext context) : base(context)
    {
    }

    public override async Task<IReadOnlyList<TeamMember>> UpdateRangeAsync(IEnumerable<TeamMember> entities)
    {
        var allTeamMembers = await GetAllAsync();

        List<TeamMember> toBeUpdated = new();
        List<TeamMember> toBeAdded = new();
        List<TeamMember> result = new();
        //check if there is any users in same team that does not exist in entites,
        //if do, delete them
        var toBeDeleted = allTeamMembers
            .Where(x => !entities
            .Any(y => y.UserId == x.UserId && y.TeamId == x.TeamId)
            && x.TeamId == entities.First().TeamId)
            .ToList();

        foreach (var entity in entities)
        {
            var oldEntity = allTeamMembers
                .FirstOrDefault(x => x.UserId == entity.UserId && x.TeamId == entity.TeamId);
            if (oldEntity != null)
            {
                AddRowData(entity, oldEntity);
                toBeUpdated.Add(entity);
            }
            else
            {
                toBeAdded.Add(entity);
            }
        }

        _context.UpdateRange(toBeUpdated);
        _context.RemoveRange(toBeDeleted);
        result.AddRange(await base.AddRangeAsync(toBeAdded));
        result.AddRange(toBeUpdated);
        
        return result;
    }

    private void AddRowData(TeamMember entity, TeamMember oldEntity)
    {
        _context.ChangeTracker.Clear();
        SetRowData(entity, oldEntity);
    }
}