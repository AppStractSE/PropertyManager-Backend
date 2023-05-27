using Core.Repository.Interfaces;
using MapsterMapper;
using MediatR;

namespace Core.Features.Commands.Category;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Domain.Category>
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;
    private readonly ICache _cache;

    public AddCategoryCommandHandler(ICategoryRepository repo, IMapper mapper, ICache cache)
    {
        _repo = repo;
        _mapper = mapper;
        _cache = cache;
    }
    public async Task<Domain.Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync("Categories:");
        var newCategory = _mapper.Map<Repository.Entities.Category>(request);
        newCategory.Id = Guid.NewGuid();
        var response = await _repo.AddAsync(newCategory);
        return _mapper.Map<Domain.Category>(response);
    }
}