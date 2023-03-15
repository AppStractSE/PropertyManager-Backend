using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(PropertyManagerContext context) : base(context)
    {

    }
}