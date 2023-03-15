using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class SubCategoryRepository : BaseRepository<SubCategory>, ISubCategoryRepository
{
    public SubCategoryRepository(PropertyManagerContext context) : base(context)
    {

    }
}