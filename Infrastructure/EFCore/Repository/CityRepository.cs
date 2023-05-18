using Core.Repository.Entities;
using Core.Repository.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class CityRepository : BaseRepository<City>, ICityRepository
{
    public CityRepository(PropertyManagerContext context) : base(context)
    {

    }
}