using Domain.Repository.Entities;
using Domain.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class ChoreCommentRepository : BaseRepository<ChoreComment>, IChoreCommentRepository
{
    public ChoreCommentRepository(PropertyManagerContext context) : base(context)
    {

    }
}