using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.City;

public class AddCityCommandHandler : IRequestHandler<AddCityCommand, Domain.City>
{
    private readonly ICityRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public AddCityCommandHandler(ICityRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Domain.City> Handle(AddCityCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync("Cities:");
        var response = await _repo.AddAsync(_mapper.Map<Repository.Entities.City>(request));
        return _mapper.Map<Domain.City>(response);
    }
}