using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class ChoreCommentRepository : BaseRepository<ChoreComment>, IChoreCommentRepository
{
    public ChoreCommentRepository(PropertyManagerContext context) : base(context)
    {

    }
}