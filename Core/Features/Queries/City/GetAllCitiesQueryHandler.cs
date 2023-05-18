using Core.Domain;
using Core.Repository.Interfaces;
using Core.Utilities;
using MapsterMapper;
using MediatR;

namespace Core.Features.Queries.Cities;

public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, IList<City>>
{
    private readonly ICityRepository _cityRepo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;
    private readonly IAreaRepository _areaRepo;

    public GetAllCitiesQueryHandler(ICityRepository cityRepo, IMapper mapper, ICache cache, IAreaRepository areaRepo)
    {
        _cityRepo = cityRepo;
        _areaRepo = areaRepo;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IList<City>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
    {
      var areas = _mapper.Map<IList<Area>>(await _areaRepo.GetAllAsync());
        var cities = _mapper.Map<IList<City>>(await _cityRepo.GetAllAsync());

        foreach (var city in cities)
        {
            city.Areas = areas.Where(x => x.CityId == city.Id.ToString()).ToList();
        }
        return cities;
        // if (_cache.Exists("Cities:"))
        // {
        //     return await _cache.GetAsync<IList<City>>("Cities:");
        // }

        // var city = _mapper.Map<IList<City>>(await _repo.GetAllAsync());
        
        // await _cache.SetAsync("Cities:", city);

        // return city;
    }
}